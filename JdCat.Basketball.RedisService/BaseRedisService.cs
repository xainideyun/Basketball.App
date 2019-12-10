using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model;
using JdCat.Basketball.Common;
using NLog;
using Microsoft.EntityFrameworkCore;

namespace JdCat.Basketball.RedisService
{
    /*   数据存储格式：
            1. 表格：Entity:[TableName]:[Id] -> [JSON]
            2. 其他无关数据库表的键：Other:[Key] -> [Value]
    */

    public class BaseRedisService : IBaseService
    {
        /// <summary>
        /// Redis连接器
        /// </summary>
        public IConnectionMultiplexer Cache { get; }
        /// <summary>
        /// SQL服务
        /// </summary>
        public BasketballDbContext Context { get; }
        public BaseRedisService(IConnectionMultiplexer cache, BasketballDbContext context)
        {
            Cache = cache;
            Context = context;
        }
        /// <summary>
        /// Redis数据库
        /// </summary>
        public IDatabase Database { get => Cache.GetDatabase(); }
        /// <summary>
        /// Redis数据库键名前缀
        /// </summary>
        public const string DatabasePrefix = "Basketball";
        ///// <summary>
        ///// 日志对象
        ///// </summary>
        //public Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 获取单个实体键名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public string KeyForCode<TEntity>(string code) where TEntity : BaseEntity
            => $"{DatabasePrefix}:Entity:{typeof(TEntity).Name}:{code}";
        /// <summary>
        /// 获取单个实体键名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public string KeyForCode<TEntity>(int id) where TEntity : BaseEntity
            => $"{DatabasePrefix}:Entity:{typeof(TEntity).Name}:{id}";
        /// <summary>
        /// 获取无关数据库表的键
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string KeyForOther(string code) => $"{DatabasePrefix}:Other:{code}";

        /// <summary>
        /// 获取多个实体键名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected RedisKey[] KeysForId<TEntity>(IEnumerable<int> ids) where TEntity : BaseEntity
            => ids.Select(a => (RedisKey)KeyForCode<TEntity>(a.ToString())).ToArray();


        public async Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.CreateTime = DateTime.Now;
            await Context.AddAsync(entity);
            var count = await Context.SaveChangesAsync();
            await SetObjectAsync(entity);
            return count;
        }
        public async Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity
        {
            await Context.AddRangeAsync(entitys);
            var count = await Context.SaveChangesAsync();
            await SetObjectAsync(entitys);
            return count;
        }
        public async Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            var key = KeyForCode<TEntity>(id);
            var obj = await Database.ObjectGetAsync<TEntity>(key);
            if (obj == null)
            {
                obj = await Context.FindAsync<TEntity>(id);
                if (obj == null) return null;
                await SetObjectAsync(obj);
            }
            return obj;
        }
        public async Task<List<TEntity>> GetAsync<TEntity>(IEnumerable<int> ids) where TEntity : BaseEntity
        {
            var keys = ids.Select(a => (RedisKey)KeyForCode<TEntity>(a)).ToArray();
            return await Database.ObjectGetAsync<TEntity>(keys);
        }
        public async Task<int> RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Context.Remove(entity);
            var count = await Context.SaveChangesAsync();
            var key = KeyForCode<TEntity>(entity.ID);
            await Database.KeyDeleteAsync(key);
            return count;
        }
        public async Task<int> RemoveAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity
        {
            Context.RemoveRange(entitys);
            var count = await Context.SaveChangesAsync();
            await Database.KeyDeleteAsync(entitys.Select(a => (RedisKey)KeyForCode<TEntity>(a.ID.ToString())).ToArray());
            return count;
        }
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, params string[] fieldNames) where TEntity : BaseEntity
        {
            if (fieldNames == null || fieldNames.Count() == 0)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                Context.Attach(entity);
                foreach (var field in fieldNames)
                {
                    Context.Entry(entity).Property(field).IsModified = true;
                }
            }
            await Context.SaveChangesAsync();
            await SetObjectAsync(entity);
            return entity;
        }
        public async Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities, params string[] fieldNames) where TEntity : BaseEntity
        {
            foreach (var entity in entities)
            {
                if (fieldNames == null || fieldNames.Count() == 0)
                {
                    Context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    Context.Attach(entity);
                    foreach (var field in fieldNames)
                    {
                        Context.Entry(entity).Property(field).IsModified = true;
                    }
                }
            }
            await Context.SaveChangesAsync();
            foreach (var item in entities)
            {
                await SetObjectAsync(item);
            }
        }
        public Task<List<TEntity>> GetListAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new()
        {
            throw new Exception("未实现接口[GetListAsync]。");
            //var ids = await GetEntityIdsAsync(paging, where, orderAsc, orderDesc);
            //var keys = ids.Select(a => (RedisKey)KeyForCode<TEntity>(a.ToString())).ToArray();
            //var entitys = await Database.ObjectGetAsync<TEntity>(keys);
            //if (keys.Length != entitys.Count)        // 如果记录数目不相等，则重新写入Redis
            //{
            //    entitys = await Context.GetListAsync(paging, where, orderAsc, orderDesc);
            //    await Database.ObjectSetAsync(entitys.Select(entity => new KeyValuePair<RedisKey, TEntity>(KeyForCode<TEntity>(entity.ID.ToString()), entity)).ToArray());
            //}
            //return entitys;
        }
        public Task<List<int>> GetEntityIdsAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new()
        {
            throw new Exception("未实现接口[GetEntityIdsAsync]。");
            //return await Context.GetEntityIdsAsync(paging, where, orderAsc, orderDesc);
        }




        /// <summary>
        /// 根据实体类型的唯一标识，获取实体对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identity"></param>
        /// <param name="identityName"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdentityAsync<TEntity>(string identity, string identityName, Func<string, Task<TEntity>> predicate) where TEntity : BaseEntity
        {
            var key = KeyForCode<TEntity>($"{identityName}:{identity}");
            var id = await Database.StringGetAsync(key);
            if (!id.IsNull)
            {
                return await GetAsync<TEntity>((int)id);
            }
            var entity = await predicate(identity);
            if (entity == null) return null;
            await Database.StringSetAsync(key, entity.ID);
            return entity;
        }
        /// <summary>
        /// 获取列表中id对应的对象列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> ListRangeObjectAsync<TEntity>(RedisKey key, PagingQuery paging = null) where TEntity : BaseEntity
        {
            var start = paging?.Skip ?? 0;
            long end = -1;
            if (paging != null) end = start + paging.PageSize - 1;
            var ids = await Database.ListRangeAsync(key, start, end);
            if (ids.Length == 0) return null;
            var entities = await Database.ObjectGetAsync<TEntity>(ids.Select(a => (RedisKey)KeyForCode<TEntity>(a)).ToArray());
            return entities;
        }

        /// <summary>
        /// 获取集合中id对应的对象列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key">键</param>
        /// <param name="start">最小score</param>
        /// <param name="stop">最大score</param>
        /// <param name="skip">跳过的记录数</param>
        /// <param name="take">获取的记录数</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        public async Task<List<TEntity>> SortedSetRangeObjectAsync<TEntity>(RedisKey key, double start = double.MinValue, double stop = double.MaxValue, long skip = 0, long take = -1, Order order = Order.Descending) where TEntity : BaseEntity
        {
            var ids = await Database.SortedSetRangeByScoreAsync(key, start: start, stop: stop, skip: skip, take: take, order: order);
            if (ids.Length == 0) return null;
            var entities = await Database.ObjectGetAsync<TEntity>(ids.Select(a => (RedisKey)KeyForCode<TEntity>(a)).ToArray());
            if(entities.Count != ids.Length)
            {
                // 如果数量不相等，表示存在已经删除的对象，在集合中删除对应的值
                var exists = entities.Select(a => a.ID).ToList();
                var removes = ids.Where(id => !exists.Contains((int)id)).ToArray();
                await Database.SortedSetRemoveAsync(key, removes);
            }
            return entities;
        }
        /// <summary>
        /// 设置单个实体对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> SetObjectAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return await Database.ObjectSetAsync(KeyForCode<TEntity>(entity.ID), entity);
        }
        /// <summary>
        /// 批量设置实体对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> SetObjectAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            var keys = entities.Select(entity => new KeyValuePair<RedisKey, TEntity>(KeyForCode<TEntity>(entity.ID.ToString()), entity)).ToArray();
            return await Database.ObjectSetAsync(keys);
        }

        #region MyRegion

        /// <summary>
        /// 获取实体对象与其他实体对象的关联列表（多对象）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="ids"></param>
        ///// <returns></returns>
        //public async Task<List<TEntity>> GetRelativeEntitysAsync<TEntity, TParent>(params string[] ids) 
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    if (ids == null || ids.Length == 0) return new List<TEntity>();
        //    var listKeys = ids.Select(a => KeyForCode<TEntity>($"{typeof(TParent).Name}:{a}")).ToList();
        //    var vals = new List<string>();
        //    foreach (var key in listKeys)
        //    {
        //        vals.AddRange((await Database.ListRangeAsync(key)).Select(a => a.ToString()));
        //    }
        //    vals = vals.Distinct().ToList();
        //    if (vals.Count == 0) return new List<TEntity>();
        //    var keys = vals.Select(a => (RedisKey)KeyForCode<TEntity>(a)).ToArray();
        //    var results = await Database.StringGetAsync(keys);
        //    var entitys = results.Select(a => a.ToObject<TEntity>()).ToList();
        //    return entitys;
        //}
        ///// <summary>
        ///// 获取实体对象与其他实体对象的关联列表
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TParent"></typeparam>
        ///// <param name="id">父级对象id</param>
        ///// <param name="paging">分页对象</param>
        ///// <returns></returns>
        //public async Task<List<TEntity>> GetRelativeEntitysAsync<TEntity, TParent>(int id, PagingQuery paging = null) 
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    var key = KeyForCode<TEntity>($"{typeof(TParent).Name}:{id}");
        //    return await ListRangeObjectAsync<TEntity>(key, paging);
        //}
        ///// <summary>
        ///// 设置实体对象与其他实体对象的关联列表
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TParent"></typeparam>
        ///// <param name="id"></param>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, params TEntity[] entities) 
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    var key = KeyForCode<TEntity>($"{typeof(TParent).Name}:{id}");
        //    await Database.ListRightPushAsync(key, entities.Select(a => (RedisValue)a.ID).ToArray());
        //}
        ///// <summary>
        ///// 设置实体对象与其他实体对象的关联列表
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TParent"></typeparam>
        ///// <param name="id"></param>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, params int[] ids)
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    var key = KeyForCode<TEntity>($"{typeof(TParent).Name}:{id}");
        //    await Database.ListRightPushAsync(key, ids.Select(a => (RedisValue)a).ToArray());
        //}
        ///// <summary>
        ///// 设置实体对象与其他实体对象的关联列表（倒序）
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TParent"></typeparam>
        ///// <param name="id"></param>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //public async Task SetRelativeEntitysReverseAsync<TEntity, TParent>(int id, params TEntity[] entities)
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    var key = KeyForCode<TEntity>($"{typeof(TParent).Name}:{id}");
        //    await Database.ListLeftPushAsync(key, entities.Select(a => (RedisValue)a.ID).ToArray());
        //}
        ///// <summary>
        ///// 设置实体对象与其他实体对象的关联列表（倒序）
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TParent"></typeparam>
        ///// <param name="id"></param>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //public async Task SetRelativeEntitysReverseAsync<TEntity, TParent>(int id, params int[] ids)
        //    where TEntity : BaseEntity, new()
        //    where TParent : BaseEntity, new()
        //{
        //    var key = KeyForCode<TEntity>($"{typeof(TParent).Name}:{id}");
        //    await Database.ListLeftPushAsync(key, ids.Select(a => (RedisValue)a).ToArray());
        //}

        #endregion

        /// <summary>
        /// 获取实体对象与其他实体对象的关联列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="id">父级对象id</param>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetRelativeEntitysAsync<TEntity, TParent>(int id, string desc = null, PagingQuery paging = null, Order order = Order.Descending)
            where TEntity : BaseEntity, new()
            where TParent : BaseEntity, new()
        {
            var key = KeyForCode<TParent>($"{desc ?? typeof(TEntity).Name}:{id}");
            return await SortedSetRangeObjectAsync<TEntity>(key, paging?.MinScore ?? double.MinValue, paging?.MaxScore ?? double.MaxValue, paging?.Skip ?? 0, paging?.PageSize ?? -1, order);
        }
        /// <summary>
        /// 设置实体对象与其他实体对象的关联列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, params TEntity[] entities)
            where TEntity : BaseEntity, new()
            where TParent : BaseEntity, new()
        {
            var key = KeyForCode<TParent>($"{typeof(TEntity).Name}:{id}");
            await Database.SortedSetAddAsync(key, entities.Select(a => new SortedSetEntry(a.ID, a.ID)).ToArray());
        }
        /// <summary>
        /// 设置实体对象与其他实体对象的关联列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, string desc = null, params TEntity[] entities)
            where TEntity : BaseEntity, new()
            where TParent : BaseEntity, new()
        {
            var key = KeyForCode<TParent>($"{desc ?? typeof(TEntity).Name}:{id}");
            await Database.SortedSetAddAsync(key, entities.Select(a => new SortedSetEntry(a.ID, a.ID)).ToArray());
        }
        /// <summary>
        /// 设置实体对象与其他实体对象的关联列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, params int[] ids)
            where TEntity : BaseEntity, new()
            where TParent : BaseEntity, new()
        {
            var key = KeyForCode<TParent>($"{typeof(TEntity).Name}:{id}");
            await Database.SortedSetAddAsync(key, ids.Select(a => new SortedSetEntry(a, a)).ToArray());
        }
        /// <summary>
        /// 设置实体对象与其他实体对象的关联列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SetRelativeEntitysAsync<TEntity, TParent>(int id, string desc = null, params int[] ids)
            where TEntity : BaseEntity, new()
            where TParent : BaseEntity, new()
        {
            var key = KeyForCode<TParent>($"{desc ?? typeof(TEntity).Name}:{id}");
            await Database.SortedSetAddAsync(key, ids.Select(a => new SortedSetEntry(a, a)).ToArray());
        }


    }
}
