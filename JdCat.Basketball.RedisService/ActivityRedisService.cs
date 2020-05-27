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
using JdCat.Basketball.Model;

namespace JdCat.Basketball.RedisService
{
    public class ActivityRedisService : BaseRedisService, IActivityService
    {
        public ActivityRedisService(IConnectionMultiplexer cache, BasketballDbContext context) : base(cache, context)
        {
        }

        public async Task<ActivityEnroll> CreateActivityAsync(ActivityEnroll activity)
        {
            var desc = "CreateActivity";
            await AddAsync(activity);
            // 缓存用户创建的活动
            await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(activity.UserInfoId, desc, activity);
            return activity;
        }

        public async Task<ActivityParticipant> JoinActivityAsync(ActivityParticipant participant)
        {
            participant.JoinTime = DateTime.Now;
            await AddAsync(participant);
            var activity = await GetAsync<ActivityEnroll>(participant.ActivityEnrollId);
            activity.Quantity++;
            var updateFields = new List<string> { nameof(activity.Quantity) };
            if (participant.Status == JoinStatus.Join)
            {
                activity.JoinQuantity++;
                updateFields.Add(nameof(activity.JoinQuantity));
            }
            else if (participant.Status == JoinStatus.Absent)
            {
                activity.AbsentQuantity++;
                updateFields.Add(nameof(activity.AbsentQuantity));
            }
            else if (participant.Status == JoinStatus.Pending)
            {
                activity.PendingQuantity++;
                updateFields.Add(nameof(activity.PendingQuantity));
            }
            await UpdateAsync(activity, updateFields.ToArray());
            await Context.SaveChangesAsync();
            // 缓存活动的参与者
            await SetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(activity.ID, participant);
            // 缓存用户参与的活动
            await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(participant.UserInfoId, activity);
            // 更新活动
            await SetObjectAsync(activity);

            return participant;
        }

        public async Task<ActivityEnroll> UpdateParticipantStatusAsync(ActivityParticipant participant)
        {
            participant.JoinTime = DateTime.Now;
            var old = await GetAsync<ActivityParticipant>(participant.ID);
            var activity = await GetAsync<ActivityEnroll>(participant.ActivityEnrollId);
            if (old.Status == JoinStatus.Absent)
            {
                activity.AbsentQuantity--;
            }
            else if (old.Status == JoinStatus.Join)
            {
                activity.JoinQuantity--;
            }
            else
            {
                activity.PendingQuantity--;
            }
            if (participant.Status == JoinStatus.Absent)
            {
                activity.AbsentQuantity++;
            }
            else if (participant.Status == JoinStatus.Join)
            {
                activity.JoinQuantity++;
            }
            else
            {
                activity.PendingQuantity++;
            }

            old.Name = participant.Name;
            old.Phone = participant.Phone;
            old.Status = participant.Status;
            old.Remark = participant.Remark;
            await Context.SaveChangesAsync();
            await SetObjectAsync(activity);
            await SetObjectAsync(old);

            return activity;
        }

        public async Task<List<ActivityEnroll>> GetUserActivitiesAsync(int userId, PagingQuery paging)
        {
            var desc = "CreateActivity";
            var activities = await GetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, desc, paging);
            return activities;
            //if (activities != null) return activities;

            //activities = await Service.GetUserActivitiesAsync(userId, paging);
            //if (activities == null || activities.Count == 0) return null;
            //// 缓存用户创建的活动
            //await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, desc, activities.ToArray());
            //return activities;
        }

        public async Task<List<ActivityEnroll>> GetJoinActivitiesAsync(int userId, PagingQuery paging)
        {
            var activities = await GetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, paging: paging);
            return activities;
            //if (activities != null) return activities;

            //activities = await Service.GetJoinActivitiesAsync(userId, paging);
            //if (activities == null || activities.Count == 0) return null;
            //// 缓存用户参与过的活动
            //await SetRelativeEntitysAsync<ActivityEnroll, UserInfo>(userId, activities.ToArray());
            //return activities;
        }

        public async Task<ActivityEnroll> GetActivityDetailAsync(int id)
        {
            var activity = await GetAsync<ActivityEnroll>(id);
            activity.ActivityParticipants = await GetActivityParticipantsAsync(id);
            return activity;
        }

        public async Task<List<ActivityParticipant>> GetActivityParticipantsAsync(int id)
        {
            var participants = await GetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(id, order: Order.Ascending);
            return participants;
            //if (participants != null) return participants;
            //participants = await Context.GetActivityParticipantsAsync(id);
            //if (participants == null) return null;
            //await SetRelativeEntitysAsync<ActivityParticipant, ActivityEnroll>(id, participants.ToArray());
            //return participants;
        }

        public async Task<bool> DeleteActivityAsync(int id)
        {
            var activity = await GetAsync<ActivityEnroll>(id);
            var result = await RemoveAsync(activity) > 0;
            var key = KeyForCode<UserInfo>($"CreateActivity:{activity.UserInfoId}");
            await Database.SortedSetRemoveAsync(key, activity.ID);
            return result;
        }


    }
}
