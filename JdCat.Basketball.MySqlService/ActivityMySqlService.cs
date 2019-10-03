using JdCat.Basketball.Model;
using JdCat.Basketball.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.Model.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JdCat.Basketball.Common;
using JdCat.Basketball.Model.Enums;

namespace JdCat.Basketball.MySqlService
{
    public class ActivityMySqlService : BaseMySqlService, IActivityService
    {
        public ActivityMySqlService(BasketballDbContext context) : base(context)
        {
        }

        public async Task<ActivityEnroll> CreateActivityAsync(ActivityEnroll activity)
        {
            await AddAsync(activity);
            return activity;
        }

        public async Task<ActivityParticipant> JoinActivityAsync(ActivityParticipant participant, ActivityEnroll activity = null)
        {
            participant.JoinTime = DateTime.Now;
            await AddAsync(participant);
            if (activity == null) activity = await GetAsync<ActivityEnroll>(participant.ActivityEnrollId);
            else Context.Attach(activity);
            activity.Quantity++;
            if (participant.Status == JoinStatus.Join) activity.JoinQuantity++;
            else if (participant.Status == JoinStatus.Absent) activity.AbsentQuantity++;
            else if (participant.Status == JoinStatus.Pending) activity.PendingQuantity++;
            await Context.SaveChangesAsync();
            return participant;
        }

        public async Task<List<ActivityEnroll>> GetUserActivitiesAsync(int userId, PagingQuery paging)
        {
            return await Context.ActivityEnrolls
                .Where(a => a.UserInfoId == userId)
                .ToListAsync();
        }

        public async Task<List<ActivityEnroll>> GetJoinActivitiesAsync(int userId, PagingQuery paging)
        {
            var ids = await Context.ActivityParticipants
                .Where(a => a.UserInfoId == userId)
                .Select(a => a.ActivityEnrollId)
                .ToListAsync();
            return await Context.ActivityEnrolls
                .Where(a => ids.Contains(a.ID))
                .ToListAsync();
        }

        public async Task<ActivityEnroll> GetActivityDetailAsync(int id)
        {
            return await Context.ActivityEnrolls
                .Include(a => a.ActivityParticipants)
                .FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<List<ActivityParticipant>> GetActivityParticipantsAsync(int id)
        {
            return await Context.ActivityParticipants.Where(a => a.ActivityEnrollId == id).ToListAsync();
        }

        public async Task<ActivityEnroll> DeleteActivityAsync(int id)
        {
            var entity = new ActivityEnroll { ID = id, Status = ActivityStatus.Delete };
            return await UpdateAsync(entity, new[] { nameof(entity.Status) });
        }

    }
}
