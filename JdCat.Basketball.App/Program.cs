using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace JdCat.Basketball.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            AutoMigration(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:6000")
                .UseStartup<Startup>()
                .UseNLog();

        /// <summary>
        /// 自动迁移
        /// </summary>
        /// <param name="host"></param>
        private static void AutoMigration(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetService<BasketballDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw new Exception("数据库自动迁移失败", ex);
                }
            }
        }
    }
}
