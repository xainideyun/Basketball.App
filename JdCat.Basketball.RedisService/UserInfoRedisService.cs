using StackExchange.Redis;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.RedisService
{
    public class UserInfoRedisService : BaseRedisService<IUserInfoService>, IUserInfoService
    {
        public UserInfoRedisService(IConnectionMultiplexer cache, IUserInfoService service) : base(cache, service)
        {
        }

        public async Task<UserInfo> GetUserByOpenIdAsync(string openid)
        {
            return await GetByIdentityAsync(openid, "OpenId", id => Service.GetUserByOpenIdAsync(id));
        }
        

    }
}
