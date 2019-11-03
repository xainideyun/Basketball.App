using JdCat.Basketball.Model;
using JdCat.Basketball.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.Model.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace JdCat.Basketball.MySqlService
{
    public class UtilMySqlService : BaseMySqlService, IUtilService
    {
        public UtilMySqlService(BasketballDbContext context) : base(context)
        {
        }

        private Logger Logger => LogManager.GetCurrentClassLogger();


        public Task SetActivityAddressAsync(int userId, Tuple<string, string, double, double> address)
        {
            throw new NotImplementedException("未实现mysql获取用户创建活动地址接口");
        }

        public Task<List<Tuple<string, string, double, double>>> GetActivityAddressesAsync(int userId)
        {
            throw new NotImplementedException("未实现mysql用户创建活动地址接口");
        }
        
        public Task<string> GetWxAccessTokenAsync(string appId, string secret)
        {
            throw new NotImplementedException("未实现mysql获取微信访问令牌接口");
        }

        public Task<string> GetWxQrCodeAsync(string appId, string secret, string scene, string path, int width = 430)
        {
            throw new NotImplementedException("未实现mysql获取小程序二维码接口");
        }

        public Task<string> GetNextMatchCodeAsync()
        {
            throw new NotImplementedException("未实现mysql获取下一个比赛编码");
        }



        public string Test()
        {
            Logger.Debug("mysql调试日志");
            Logger.Error(new Exception("登录失败"), "mysql系统出错了");
            
            return "ok";
        }
    }
}
