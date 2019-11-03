using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Model.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JdCat.Basketball.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : BaseController<IActivityService>
    {
        public ActivityController(IActivityService service) : base(service)
        {
        }

        /// <summary>
        /// 获取用户创建的比赛
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paging"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("create/{id}")]
        public async Task<ActionResult<ApiResult<List<ActivityEnroll>>>> GetUserActivities(int id, [FromQuery]PagingQuery paging)
        {
            var activities = await Service.GetUserActivitiesAsync(id, paging);
            return new ApiResult<List<ActivityEnroll>> { Code = 0, Result = activities };
        }

        /// <summary>
        /// 创建报名活动
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult<ActivityEnroll>>> Post([FromBody]ActivityEnroll activity, [FromServices]IUtilService util)
        {
            activity.Status = ActivityStatus.Doing;
            await Service.CreateActivityAsync(activity);
            // 活动创建成功，保存创建的地址
            var address = new Tuple<string, string, double, double>(activity.Location, activity.Address, activity.Lng, activity.Lat);
            await util.SetActivityAddressAsync(activity.UserInfoId, address);
            return new ApiResult<ActivityEnroll> { Result = activity };
        }

        /// <summary>
        /// 获取报名活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<ActivityEnroll>>> GetActivityDetail(int id)
        {
            var activity = await Service.GetActivityDetailAsync(id);
            return new ApiResult<ActivityEnroll> { Result = activity };
        }

        /// <summary>
        /// 获取活动参与者列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("participants/{id}")]
        public async Task<ActionResult<ApiResult<List<ActivityParticipant>>>> GetActivityParticipants(int id)
        {
            var list = await Service.GetActivityParticipantsAsync(id);
            return new ApiResult<List<ActivityParticipant>> { Result = list };
        }

        /// <summary>
        /// 参加活动
        /// </summary>
        /// <param name="participant"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost("join")]
        public async Task<ActionResult<ApiResult<ActivityParticipant>>> PostActivityParticipant([FromBody]ActivityParticipant participant)
        {
            await Service.JoinActivityAsync(participant);
            return new ApiResult<ActivityParticipant> { Result = participant };
        }

        /// <summary>
        /// 更新活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activity"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<ActivityEnroll>>> UpdateActivity(int id, [FromBody]ActivityEnroll activity)
        {
            var entity = await Service.GetAsync<ActivityEnroll>(id);
            entity.Title = activity.Title;
            entity.ActivityTime = activity.ActivityTime;
            entity.Location = activity.Location;
            entity.Address = activity.Address;
            entity.Lng = activity.Lng;
            entity.Lat = activity.Lat;
            entity.Remark = activity.Remark;
            await Service.UpdateAsync(entity, new[] { nameof(activity.Title), nameof(activity.ActivityTime), nameof(activity.Location), nameof(activity.Address), nameof(activity.Lng), nameof(activity.Lat), nameof(activity.Remark) });

            return new ApiResult<ActivityEnroll> { Message = "ok" };
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<ActivityEnroll>>> DeleteActivity(int id)
        {
            await Service.DeleteActivityAsync(id);
            return new ApiResult<ActivityEnroll> { Message = "ok" };
        }

        /// <summary>
        /// 获取参与者
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("participant/{id}")]
        public async Task<ActionResult<ApiResult<ActivityParticipant>>> GetParticipant(int id)
        {
            var entity = await Service.GetAsync<ActivityParticipant>(id);
            return new ApiResult<ActivityParticipant> { Result = entity };
        }

        /// <summary>
        /// 获取用户参与过的活动
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("join/{id}")]
        public async Task<ActionResult<ApiResult<List<ActivityEnroll>>>> GetJoinActivities(int id, [FromQuery]PagingQuery paging)
        {
            var list = await Service.GetJoinActivitiesAsync(id, paging);
            return new ApiResult<List<ActivityEnroll>> { Result = list };
        }

        /// <summary>
        /// 获取用户使用过的地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="util"></param>
        /// <returns></returns>
        [HttpGet("address/{id}")]
        public async Task<ActionResult<ApiResult<List<Tuple<string, string, double, double>>>>> GetAddress(int id, [FromServices]IUtilService util)
        {
            return new ApiResult<List<Tuple<string, string, double, double>>> { Code = 0, Result = await util.GetActivityAddressesAsync(id) };
        }

    }
}