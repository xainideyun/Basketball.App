using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
        public async Task<ActionResult<ApiResult<UserInfo>>> GrantUserInfo([FromBody]UserInfo user, [FromServices]IUserInfoService service)
        {
            var userinfo = await service.GetAsync<UserInfo>(user.ID);
            userinfo.NickName = user.NickName;
            userinfo.FaceUrl = user.FaceUrl;
            userinfo.Country = user.Country;
            userinfo.Province = user.Province;
            userinfo.City = user.City;
            userinfo.Gender = user.Gender;
            userinfo.Phone = user.Phone;
            await service.UpdateAsync(userinfo, new[] { nameof(user.NickName), nameof(user.FaceUrl), nameof(user.Country), nameof(user.Province), nameof(user.City), nameof(user.Gender), nameof(user.Phone) });
            return new ApiResult<UserInfo> { Message = "request:ok", Result = user };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<string>>> UpdateUserinfo(int id, [FromBody]UserInfo user, [FromServices]IUserInfoService service)
        {
            await service.UpdateAsync(user, new[] { nameof(user.NickName), nameof(user.Name), nameof(user.FaceUrl), nameof(user.Phone) });
            return new ApiResult<string> { Message = "request:ok", Result = "ok" };
        }

    }
}