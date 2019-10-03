using JdCat.Basketball.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JdCat.Basketball.IService
{
    public interface IUtilService : IBaseService
    {
        /// <summary>
        /// 设置用户活动地址
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="address">地址信息：地点、详细地址、经度、纬度</param>
        /// <returns></returns>
        Task SetActivityAddressAsync(int userId, Tuple<string, string, double, double> address);
        /// <summary>
        /// 获取用户活动地址
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<List<Tuple<string, string, double, double>>> GetActivityAddressesAsync(int userId);
        /// <summary>
        /// 获取微信访问令牌
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        Task<string> GetWxAccessTokenAsync(string appId, string secret);
        /// <summary>
        /// 获取微信小程序二维码
        /// </summary>
        /// <param name="appId">小程序id</param>
        /// <param name="secret">密钥</param>
        /// <param name="scene">参数</param>
        /// <param name="path">页面路径</param>
        /// <param name="width">小程序宽度</param>
        /// <returns></returns>
        Task<string> GetWxQrCodeAsync(string appId, string secret, string scene, string path, int width = 430);



        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        string Test();
    }
}
