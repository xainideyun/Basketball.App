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

    }
}
