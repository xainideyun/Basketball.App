using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace JdCat.Basketball.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResult<UserInfo>>> Login([FromQuery]string code, [FromServices]IUserInfoService service)
        {
            var res = await WeixinHelper.GetLoginInfoAsync(code);
            var json = JObject.Parse(res);
            var result = new ApiResult<UserInfo>();
            var openId = json["openid"].Value<string>();
            var user = await service.GetUserByOpenIdAsync(openId);
            if (user == null)
            {
                // 用户第一次登陆
                user = new UserInfo { OpenId = openId };
                await service.AddAsync(user);
            }
            result.Result = user;
            result.Message = "requst:ok";
            return result;
        }

        /// <summary>
        /// 授权用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult<UserInfo>>> GrantUserInfo([FromBody]UserInfo user, [FromServices]IUserInfoService service, [FromServices]IHostingEnvironment hostingEnvironment)
        {
            var userinfo = await service.GetAsync<UserInfo>(user.ID);
            userinfo.NickName = user.NickName;
            userinfo.FaceUrl = user.FaceUrl;
            userinfo.Country = user.Country;
            userinfo.Province = user.Province;
            userinfo.City = user.City;
            userinfo.Gender = user.Gender;
            userinfo.Phone = user.Phone;
            await SaveFaceAsync(userinfo);      // 保存用户头像
            await service.UpdateAsync(userinfo, new[] { nameof(user.NickName), nameof(user.FaceUrl), nameof(user.Country), nameof(user.Province), nameof(user.City), nameof(user.Gender), nameof(user.Phone) });
            return new ApiResult<UserInfo> { Message = "request:ok", Result = userinfo };
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="user"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<string>>> UpdateUserinfo(int id, [FromBody]UserInfo user, [FromServices]IUserInfoService service)
        {
            await service.UpdateAsync(user, new[] { nameof(user.NickName), nameof(user.Name), nameof(user.FaceUrl), nameof(user.Phone) });
            return new ApiResult<string> { Message = "request:ok", Result = "ok" };
        }

        /// <summary>
        /// 获取我的比赛数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        [HttpGet("match/{id}")]
        public async Task<ActionResult<object>> GetMyMatchs(int id, [FromQuery]PagingQuery paging, [FromServices]IUserInfoService service)
        {
            var players = await service.GetPlayersAsync(id, paging);
            if (players == null) return new List<object>();
            var ids = players.Select(a => a.MatchId).ToList();
            var matchs = await service.GetAsync<Match>(ids);
            players.ForEach(a => a.Match = matchs.FirstOrDefault(b => b.ID == a.MatchId));

            return players.Select(a => new { Host = a.Match.HostName, Visitor = a.Match.VisitorName, a.Match.HostScore, a.Match.VisitorScore, a.Score, a.Match.StartTime, Id = a.ID }).ToList();
        }

        /// <summary>
        /// 获取球员数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("player/{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id, [FromServices]IMatchService service)
        {
            return await service.GetAsync<Player>(id);
        }


        /// <summary>
        /// 保存用户头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task SaveFaceAsync(UserInfo user)
        {
            var hostingEnvironment = HttpContext.RequestServices.GetService<IHostingEnvironment>();
            var dir = Path.Combine(hostingEnvironment.WebRootPath, "Face");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filename = user.OpenId + ".jpg";
            var filepath = Path.Combine(dir, filename);
            using (var client = new HttpClient())
            {
                var file = await client.GetByteArrayAsync(user.FaceUrl);
                //var buffer = new byte[file.Length];
                //await file.ReadAsync(buffer, 0, buffer.Length);
                using (var stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await stream.WriteAsync(file, 0, file.Length);
                }
            }
            user.FaceUrl = AppSetting.Setting.FileDomain + "/Face/" + filename;
        }
    }
}