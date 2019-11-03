using StackExchange.Redis;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using JdCat.Basketball.Model;

namespace JdCat.Basketball.RedisService
{
    public class UtilRedisService : BaseRedisService, IUtilService
    {
        public UtilRedisService(IConnectionMultiplexer cache, BasketballDbContext context) : base(cache, context)
        {
        }


        private Logger Logger => LogManager.GetCurrentClassLogger();


        public async Task SetActivityAddressAsync(int userId, Tuple<string, string, double, double> address)
        {
            var key = KeyForOther($"ActivityAddress:{userId}");
            var list = await GetActivityAddressesAsync(userId);
            var entity = list?.FirstOrDefault(a => a.Item1 == address.Item1);
            if (entity == null)
            {
                await Database.SortedSetAddAsync(key, address.ToJson(), 1);
                return;
            }
            await Database.SortedSetIncrementAsync(key, address.ToJson(), 1);
        }

        public async Task<List<Tuple<string, string, double, double>>> GetActivityAddressesAsync(int userId)
        {
            var key = KeyForOther($"ActivityAddress:{userId}");
            var list = await Database.SortedSetRangeByScoreAsync(key);
            if (list.Length == 0) return null;
            return list.Select(a => a.ToObject<Tuple<string, string, double, double>>()).ToList();
        }

        public async Task<string> GetWxAccessTokenAsync(string appId, string secret)
        {
            var key = KeyForOther($"Weixin:AccessToken:{appId}");
            var now = DateTime.Now;
            var ret = await Database.StringGetAsync(key);
            JObject json = null;

            if (!ret.IsNull)
            {
                json = JObject.Parse(ret);
                if(json["expire_time"].Value<DateTime>() > now)
                {
                    return json["access_token"].Value<string>();
                }
            }

            var token = await WeixinHelper.GetAccessTokenAsync(appId, secret);
            json = JObject.Parse(token);
            if (json["errcode"] != null && json["errcode"].Value<int>() > 0)
            {
                throw new Exception($"获取令牌错误，{json["errmsg"].Value<string>()}");
            }
            json.Add("expire_time", now.AddSeconds(7000));      // 距离获取时间7000秒后过期
            await Database.StringSetAsync(key, json.ToJson());
            return json["access_token"].Value<string>();
        }

        public async Task<string> GetWxQrCodeAsync(string appId, string secret, string scene, string path, int width = 430)
        {
            var key = KeyForOther($"File:Qrcode:{path}_{scene}_{width}");
            var fileUrl = await Database.StringGetAsync(key);
            if(!fileUrl.IsNull)
            {
                return fileUrl.ToString();
            }

            var token = await GetWxAccessTokenAsync(appId, secret);
            var result = await WeixinHelper.CreateQRCodeAsync(token, scene, path, width);
            if(result.Code > 0)
            {
                throw new Exception($"小程序二维码请求错误，{result.Message}");
            }
            var filename = Guid.NewGuid().ToString().ToLower() + ".jpg";
            var dir = Path.Combine(Environment.CurrentDirectory, "wwwroot", "qrcode");
            Directory.CreateDirectory(dir);
            var filepath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "qrcode", filename);
            using (var file = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                file.Write(result.Result, 0, result.Result.Length);
            }
            var url = $"{AppSetting.Setting.FileDomain}/qrcode/{filename}";
            await Database.StringSetAsync(key, url);
            return url;
        }

        public async Task<string> GetNextMatchCodeAsync()
        {
            var key = KeyForOther($"Match:Code");
            var num = await Database.StringIncrementAsync(key);
            var code = $"{DateTime.Now:yyyyMMdd}{num}{UtilHelper.RandNum()}";
            return code;
        }




        public string Test()
        {
            Logger.Debug("redis调试日志");
            Logger.Error(new Exception("登录失败"), "redis系统出错了");

            Thread.Sleep(1000);
            

            return "ok";
        }

    }
}
