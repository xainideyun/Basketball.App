using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace JdCat.Basketball.Common
{
    public static class RedisExtensions
    {
        /// <summary>
        /// 设置Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool ObjectSet<T>(this IDatabase database, RedisKey key, T obj, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("设置Redis对象引用为空");
            }
            return database.StringSet(key, obj.ToJson(), expiry, when, flags);
        }
        /// <summary>
        /// 设置Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<bool> ObjectSetAsync<T>(this IDatabase database, RedisKey key, T obj, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("设置Redis对象引用为空");
            }
            return await database.StringSetAsync(key, obj.ToJson(), expiry, when, flags);
        }

        /// <summary>
        /// 批量设置Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="values"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool ObjectSet<T>(this IDatabase database, KeyValuePair<RedisKey, T>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            var list = values.Select(a => new KeyValuePair<RedisKey, RedisValue>(a.Key, a.Value.ToJson())).ToArray();
            return database.StringSet(list, when, flags);
        }
        /// <summary>
        /// 批量设置Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="values"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<bool> ObjectSetAsync<T>(this IDatabase database, KeyValuePair<RedisKey, T>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            var list = values.Select(a => new KeyValuePair<RedisKey, RedisValue>(a.Key, a.Value.ToJson())).ToArray();
            return await database.StringSetAsync(list, when, flags);
        }

        /// <summary>
        /// 根据键名获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static T ObjectGet<T>(this IDatabase database, string key, CommandFlags flags = CommandFlags.None) where T : class
        {
            var value = database.StringGet(key, flags);
            if (value.IsNull) return default(T);
            return value.ToObject<T>();
        }
        /// <summary>
        /// 根据键名获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<T> ObjectGetAsync<T>(this IDatabase database, string key, CommandFlags flags = CommandFlags.None) where T : class
        {
            var value = await database.StringGetAsync(key, flags);
            if (value.IsNull) return default(T);
            return value.ToObject<T>();
        }

        /// <summary>
        /// 根据键名批量获取Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="keys"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static List<T> ObjectGet<T>(this IDatabase database, RedisKey[] keys, CommandFlags flags = CommandFlags.None) where T : class
        {
            var value = database.StringGet(keys, flags);
            var vals = value.Where(a => !a.IsNull);
            return vals.Select(a => a.ToObject<T>()).ToList();
        }
        /// <summary>
        /// 根据键名批量获取Redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="keys"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<List<T>> ObjectGetAsync<T>(this IDatabase database, RedisKey[] keys, CommandFlags flags = CommandFlags.None) where T : class
        {
            var value = await database.StringGetAsync(keys, flags);
            var vals = value.Where(a => !a.IsNull);
            return vals.Select(a => a.ToObject<T>()).ToList();
        }

        /// <summary>
        /// 列表尾部插入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long ListRightPush<T>(this IDatabase database, RedisKey key, T value, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            return database.ListRightPush(key, value.ToJson(), when, flags);
        }
        /// <summary>
        /// 列表尾部插入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync<T>(this IDatabase database, RedisKey key, T value, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
        {
            return await database.ListRightPushAsync(key, value.ToJson(), when, flags);
        }

        /// <summary>
        /// 列表尾部插入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long ListRightPush<T>(this IDatabase database, RedisKey key, IEnumerable<T> values, CommandFlags flags = CommandFlags.None) where T : class
        {
            var list = values.Select(a => (RedisValue)a.ToJson()).ToArray();
            return database.ListRightPush(key, list, flags);
        }
        /// <summary>
        /// 列表尾部插入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync<T>(this IDatabase database, RedisKey key, IEnumerable<T> values, CommandFlags flags = CommandFlags.None) where T : class
        {
            var list = values.Select(a => (RedisValue)a.ToJson()).ToArray();
            return await database.ListRightPushAsync(key, list, flags);
        }



    }
}
