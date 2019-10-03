using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.Common
{
    public static class HttpHelper
    {
        /// <summary>
        /// 发送请求，返回字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<string> RequestAsync(string url, object content = null, string method = "post")
        {
            var result = await SendRequestAsync(url, content, method);
            return await result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 发送请求，返回二进制文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<ApiResult<byte[]>> RequestFileAsync(string url, object content = null, string method = "post")
        {
            var data = await SendRequestAsync(url, content, method);
            var buffer = await data.Content.ReadAsByteArrayAsync();
            var result = new ApiResult<byte[]>();
            if(buffer != null)
            {
                result.Result = buffer;
                return result;
            }
            var ret = await data.Content.ReadAsStringAsync();
            var json = JObject.Parse(ret);
            result.Code = json["errCode"].Value<int>();
            result.Message = json["errMsg"].Value<string>();
            return result;
        }





        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendRequestAsync(string url, object content, string method)
        {
            method = method.ToLower();
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                HttpContent body = null;
                if (content != null)
                {
                    if (content is string)
                    {
                        body = new StringContent(content.ToString());
                    }
                    else
                    {
                        body = new StringContent(JsonConvert.SerializeObject(content));
                    }
                }
                switch (method)
                {
                    case "get":
                        result = await client.GetAsync(url);
                        break;
                    case "post":
                        result = await client.PostAsync(url, body);
                        break;
                    case "put":
                        result = await client.PutAsync(url, body);
                        break;
                    case "delete":
                        result = await client.DeleteAsync(url);
                        break;
                    default:
                        throw new Exception($"不支持方法{method}");
                }
                if (body != null)
                {
                    body.Dispose();
                }
            }
            result.EnsureSuccessStatusCode();
            return result;
        }
    }
}
