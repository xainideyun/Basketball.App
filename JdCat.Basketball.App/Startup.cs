using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JdCat.Basketball.App.Middles;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model;
using JdCat.Basketball.RedisService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace JdCat.Basketball.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var setting = Configuration.GetSection("setting");
            setting.Bind(AppSetting.Setting);
            setting.Bind(WeixinHelper.Weixin);

            NLog.LogManager.LoadConfiguration("NLog.config");
            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("sqlConn");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(option =>
                {
                    option.SerializerSettings.ReferenceLoopHandling = AppSetting.JsonSetting.ReferenceLoopHandling;
                    option.SerializerSettings.DateFormatString = AppSetting.JsonSetting.DateFormatString;
                    option.SerializerSettings.ContractResolver = AppSetting.JsonSetting.ContractResolver;
                });

            // 注册mysql数据库
            services.AddDbContext<BasketballDbContext>(options => options.UseMySql(Configuration["connectionStrings:sqlConn"], b => b.MigrationsAssembly("JdCat.Basketball.Model")));

            // 注册redis连接器
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration["connectionStrings:redisConn"]));

            // 配置依赖
            //services.AddScoped<IUtilService, UtilRedisService>(a => new UtilRedisService(a.GetService<IConnectionMultiplexer>(), new UtilMySqlService(a.GetService<BasketballDbContext>())));
            //services.AddScoped<IUserInfoService, UserInfoRedisService>(a => new UserInfoRedisService(a.GetService<IConnectionMultiplexer>(), new UserInfoMySqlService(a.GetService<BasketballDbContext>())));
            //services.AddScoped<IActivityService, ActivityRedisService>(a => new ActivityRedisService(a.GetService<IConnectionMultiplexer>(), new ActivityMySqlService(a.GetService<BasketballDbContext>())));
            //services.AddScoped<IMatchService, MatchRedisService>(a => new MatchRedisService(a.GetService<IConnectionMultiplexer>(), new MatchMySqlService(a.GetService<BasketballDbContext>())));

            services.AddScoped<IUtilService, UtilRedisService>(a => new UtilRedisService(a.GetService<IConnectionMultiplexer>(), a.GetService<BasketballDbContext>()));
            services.AddScoped<IUserInfoService, UserInfoRedisService>(a => new UserInfoRedisService(a.GetService<IConnectionMultiplexer>(), a.GetService<BasketballDbContext>()));
            services.AddScoped<IActivityService, ActivityRedisService>(a => new ActivityRedisService(a.GetService<IConnectionMultiplexer>(), a.GetService<BasketballDbContext>()));
            services.AddScoped<IMatchService, MatchRedisService>(a => new MatchRedisService(a.GetService<IConnectionMultiplexer>(), a.GetService<BasketballDbContext>()));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseErrorHandling();                 // 异常处理

            app.UseCors(MyAllowSpecificOrigins);    // 跨域处理

            app.UseMvc();

        }
    }
}
