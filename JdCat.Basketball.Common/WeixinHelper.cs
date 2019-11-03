using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.Common
{
    public class WeixinHelper
    {
        /// <summary>
        /// 用户登录解码api
        /// </summary>
        private const string LOGIN_URL = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
        /// <summary>
        /// 获取访问令牌api
        /// </summary>
        private const string ACCESS_TOKEN = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// 创建小程序二维码api
        /// </summary>
        private const string CREATE_QRCODE = "https://api.weixin.qq.com/wxa/getwxacode?access_token={0}";
        //private const string CREATE_QRCODE = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";

        static WeixinHelper()
        {
            Weixin = new WeixinHelper();
        }
        public string AppId { get; set; }
        public string Secret { get; set; }

        public static WeixinHelper Weixin { get; private set; }

        /// <summary>
        /// 获取用户登录信息（获取openid）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static async Task<string> GetLoginInfoAsync(string code, string appId = null, string secret = null)
        {
            return await HttpHelper.RequestAsync(string.Format(LOGIN_URL, appId ?? Weixin.AppId, secret ?? Weixin.Secret, code), method: "get");
        }

        /// <summary>
        /// 获取微信访问令牌
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static async Task<string> GetAccessTokenAsync(string appId = null, string secret = null)
        {
            return await HttpHelper.RequestAsync(string.Format(ACCESS_TOKEN, appId ?? Weixin.AppId, secret ?? Weixin.Secret), method: "get");
        }

        /// <summary>
        /// 创建小程序二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static async Task<ApiResult<byte[]>> CreateQRCodeAsync(string token, string scene, string path, int width = 430)
        {
            //return await HttpHelper.RequestFileAsync(string.Format(CREATE_QRCODE, token), new { path, width, scene });
            return await HttpHelper.RequestFileAsync(string.Format(CREATE_QRCODE, token), new { path = path + scene, width });
            //return await HttpHelper.RequestFileAsync(string.Format(CREATE_QRCODE, token), new { path = path + scene, width });
        }

    }
}
