using JdCat.Basketball.Common;
using JdCat.Basketball.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.IService
{
    public interface IBaseService
    {
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="entity"></param>
        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entitys"></param>
        Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity;
        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="entity"></param>
        Task<int> RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entitys"></param>
        Task<int> RemoveAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : BaseEntity;
        /// <summary>
        /// 修改实体对象
        /// </summary>
        /// <param name="entity"></param>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, params string[] fieldNames) where TEntity : BaseEntity;
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities, params string[] fieldNames) where TEntity : BaseEntity;
        /// <summary>
        /// 根据id获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity;
        /// <summary>
        /// 根据id获取实体对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAsync<TEntity>(IEnumerable<int> ids) where TEntity : BaseEntity;
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new();
        /// <summary>
        /// 获取实体对象id列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="paging">分页对象</param>
        /// <param name="where">筛选条件</param>
        /// <param name="orderAsc"></param>
        /// <param name="orderDesc"></param>
        /// <returns></returns>
        Task<List<int>> GetEntityIdsAsync<TEntity>(PagingQuery paging = null, Func<TEntity, bool> where = null, Func<TEntity, object> orderAsc = null, Func<TEntity, object> orderDesc = null) where TEntity : BaseEntity, new();
    }
}
