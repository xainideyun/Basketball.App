using JdCat.Basketball.Common;
using JdCat.Basketball.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.IService
{
    public interface IActivityService : IBaseService
    {
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task<ActivityEnroll> CreateActivityAsync(ActivityEnroll activity);
        /// <summary>
        /// 参与活动
        /// </summary>
        /// <param name="participant"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task<ActivityParticipant> JoinActivityAsync(ActivityParticipant participant);
        /// <summary>
        /// 获取用户创建的活动
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<List<ActivityEnroll>> GetUserActivitiesAsync(int userId, PagingQuery paging);
        /// <summary>
        /// 获取用户报名过的活动
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<List<ActivityEnroll>> GetJoinActivitiesAsync(int userId, PagingQuery paging);
        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActivityEnroll> GetActivityDetailAsync(int id);
        /// <summary>
        /// 获取活动参与者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<ActivityParticipant>> GetActivityParticipantsAsync(int id);
        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteActivityAsync(int id);
    }
}
