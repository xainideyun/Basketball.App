using JdCat.Basketball.Common;
using JdCat.Basketball.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.IService
{
    public interface IUserInfoService : IBaseService
    {
        /// <summary>
        /// 根据openid获取用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        Task<UserInfo> GetUserByOpenIdAsync(string openid);

        /// <summary>
        /// 获取用户的球员数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<List<Player>> GetPlayersAsync(int userId, PagingQuery paging);

    }
}
