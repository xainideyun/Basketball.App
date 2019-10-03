using StackExchange.Redis;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using JdCat.Basketball.Model.Enums;
using System.Linq;

namespace JdCat.Basketball.RedisService
{
    public class ActivityRedisService : BaseRedisService<IActivityService>, IActivityService
    {
        public ActivityRedisService(IConnectionMultiplexer cache, IActivityService service) : base(cache, service)
        {
        }

        public async Task<ActivityEnroll> CreateActivityAsync(ActivityEnroll activity)
        {
            await Service.CreateActivityAsync(activity);
            await SetObjectAsync(activity);
            // 缓存用户创建的活动
            await Database.ListRightPushAsync(KeyForCode<UserInfo>($"CreateActivity:{activity.UserInfoId}"), activity.ID);
            return activity;
        }

        public async Task<ActivityParticipant> JoinActivityAsync(ActivityParticipant participant, ActivityEnroll activity = null)
        {
            if (activity == null) activity = await GetAsync<ActivityEnroll>(participant.ActivityEnrollId);
            await Service.JoinActivityAsync(participant, activity);
            await SetObjectAsync(participant);
            // 缓存活动的参与者
            await SetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(activity.ID, participant);
            // 缓存用户参与的活动
            await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(participant.UserInfoId, activity);
            // 更新活动
            await SetObjectAsync(activity);

            return participant;
        }

        public async Task<List<ActivityEnroll>> GetUserActivitiesAsync(int userId, PagingQuery paging)
        {
            var activities = await ListRangeObjectAsync<ActivityEnroll>(KeyForCode<UserInfo>($"CreateActivity:{userId}"), paging);
            if (activities != null) return activities;

            activities = await Service.GetUserActivitiesAsync(userId, paging);
            if (activities == null) return null;
            // 缓存用户创建的活动
            await Database.ListRightPushAsync(KeyForCode<UserInfo>($"CreateActivity:{userId}"), activities.Select(a => (RedisValue)a.ID).ToArray());
            return activities;
        }

        public async Task<List<ActivityEnroll>> GetJoinActivitiesAsync(int userId, PagingQuery paging)
        {
            var activities = await GetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, paging);
            if (activities != null) return activities;

            activities = await Service.GetJoinActivitiesAsync(userId, paging);
            if (activities == null) return null;
            // 缓存用户参与过的活动
            await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, activities.ToArray());
            return activities;
        }

        public async Task<ActivityEnroll> GetActivityDetailAsync(int id)
        {
            var activity = await GetAsync<ActivityEnroll>(id);
            activity.ActivityParticipants = await GetActivityParticipantsAsync(id);
            return activity;
        }

        public async Task<List<ActivityParticipant>> GetActivityParticipantsAsync(int id)
        {
            var participants = await GetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(id);
            if (participants != null) return participants;
            participants = await Service.GetActivityParticipantsAsync(id);
            if (participants == null) return null;
            await SetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(id, participants.ToArray());
            return participants;
        }

        public async Task<ActivityEnroll> DeleteActivityAsync(int id)
        {
            var activity = await GetAsync<ActivityEnroll>(id);
            var entity = await Service.DeleteActivityAsync(id);
            activity.Status = entity.Status;
            await SetObjectAsync(activity);
            return activity;
        }


    }
}
