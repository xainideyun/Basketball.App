using StackExchange.Redis;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JdCat.Basketball.RedisService
{
    public class UserInfoRedisService : BaseRedisService, IUserInfoService
    {
        public UserInfoRedisService(IConnectionMultiplexer cache, BasketballDbContext context) : base(cache, context)
        {
        }

        public async Task<UserInfo> GetUserByOpenIdAsync(string openid)
        {
            return await GetByIdentityAsync(openid, "OpenId", async id => await Context.UserInfos.FirstOrDefaultAsync(a => a.OpenId == openid));
        }
        

    }
}
