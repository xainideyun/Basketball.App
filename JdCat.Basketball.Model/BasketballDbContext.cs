using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Model.SeedDatas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JdCat.Basketball.Model
{
    public class BasketballDbContext : DbContext
    {

        public BasketballDbContext(DbContextOptions<BasketballDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActivityEnroll> ActivityEnrolls { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<MatchLog> MatchLog { get; set; }
        public DbSet<AccessAuthority> AccessAuthorities { get; set; }
        public DbSet<NLogInfo> NLogInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 添加FluentAPI配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 种子数据
            var baseSeedName = typeof(BaseSeedData).FullName;
            var seeds = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType.FullName == baseSeedName);
            foreach (var item in seeds)
            {
                dynamic seed = Activator.CreateInstance(item, modelBuilder);
                seed.HasData();
            }

            // 配置映射
            var mappings = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);
            foreach (var type in mappings)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

    }
}
