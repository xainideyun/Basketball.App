using Microsoft.EntityFrameworkCore;
using JdCat.Basketball.Common;
using JdCat.Basketball.Model;
using JdCat.Basketball.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace JdCat.Basketball.MySqlService
{
    public class BaseMySqlService : IBaseService
    {
        public BasketballDbContext Context { get; }

        public BaseMySqlService(BasketballDbContext context)
        {
            Context = context;
        }
        public async Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity.ID > 0) return 0;
            entity.CreateTime = DateTime.Now;
            await Context.AddAsync(entity);
            return await Context.SaveChangesAsync();
        }
        public async Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity
        {
            var now = DateTime.Now;
            var list = entitys.Where(a => a.ID == 0).ToList();
            list.ForEach(a => a.CreateTime = now);
            await Context.AddRangeAsync(list);
            return await Context.SaveChangesAsync();
        }
        public async Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
        }
        public async Task<int> RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Context.Remove(entity);
            return await Context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity
        {
            Context.RemoveRange(entitys);
            return await Context.SaveChangesAsync();
        }
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> fieldNames = null) where TEntity : BaseEntity
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
            //Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<TEntity>> GetListAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (where != null) query = (IQueryable<TEntity>)query.Where(where);
            if (orderAsc != null) query = (IQueryable<TEntity>)query.OrderBy(orderAsc);
            if (orderDesc != null) query = (IQueryable<TEntity>)query.OrderByDescending(orderDesc);
            if (paging == null) return await query.ToListAsync();
            if (paging.RecordCount == 0)
            {
                paging.RecordCount = await query.CountAsync();
            }
            return await query.AsNoTracking().Skip(paging.Skip).Take(paging.PageSize).ToListAsync();
        }
        public async Task<List<int>> GetEntityIdsAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (where != null) query = (IQueryable<TEntity>)query.Where(where);
            if (orderAsc != null) query = (IQueryable<TEntity>)query.OrderBy(orderAsc);
            if (orderDesc != null) query = (IQueryable<TEntity>)query.OrderByDescending(orderDesc);
            if (paging == null) return await query.Select(a => a.ID).ToListAsync();
            if (paging.RecordCount == 0)
            {
                paging.RecordCount = await query.CountAsync();
            }
            return await query.Select(a => a.ID).Skip(paging.Skip).Take(paging.PageSize).ToListAsync();
        }
    }
}
