using JdCat.Basketball.Model;
using JdCat.Basketball.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.Model.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JdCat.Basketball.MySqlService
{
    public class UserInfoMySqlService : BaseMySqlService, IUserInfoService
    {
        public UserInfoMySqlService(BasketballDbContext context) : base(context)
        {
        }

        public async Task<UserInfo> GetUserByOpenIdAsync(string openid)
        {
            return await Context.UserInfos.FirstOrDefaultAsync(a => a.OpenId == openid);
        }

    }
}
