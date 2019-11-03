using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.Common
{
    public static class TypeExtension
    {
        /// <summary>
        /// 将对象转化为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj) where T: class
        {
            return JsonConvert.SerializeObject(obj, AppSetting.JsonSetting);
        }

        /// <summary>
        /// 将json字符串转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string obj) where T: class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        /// <summary>
        /// 将RedisValue转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToObject<T>(this RedisValue val)
        {
            return JsonConvert.DeserializeObject<T>(val);
        }

        /// <summary>
        /// 将string列表转化为RedisValue数组
        /// </summary>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static RedisValue[] ToRedisValue(this IEnumerable<string> vals)
        {
            return vals.Select(a => (RedisValue)a).ToArray();
        }

        /// <summary>
        /// 遍历列表，执行函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> predicate)
        {
            foreach (var item in list)
            {
                predicate(item);
            }
            return list;
        }

        /// <summary>
        /// 遍历列表，执行函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> predicate)
        {
            foreach (var item in list)
            {
                await predicate(item);
            }
            return list;
        }

        /// <summary>
        /// 将字符串转化为MD5码
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static string ToMd5(this string input, Encoding encode = null)
        {
            if (encode == null) encode = Encoding.UTF8;
            var md5 = new MD5CryptoServiceProvider();
            var bytResult = md5.ComputeHash(encode.GetBytes(input));
            var strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult.ToLower();
        }

        /// <summary>
        /// 将字符串转化为二进制数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 将二进制数组转化为字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] buffer, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetString(buffer);
        }

        /// <summary>
        /// 将时间转化为时间戳（秒数）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds) * 1000;
        }

    }
}
