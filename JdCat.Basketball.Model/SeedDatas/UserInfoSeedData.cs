using JdCat.Basketball.Common;
using JdCat.Basketball.Model.Domain;
using JdCat.Basketball.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JdCat.Basketball.Model.SeedDatas
{
    public class UserInfoSeedData : BaseSeedData
    {
        public UserInfoSeedData(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void HasData()
        {
            var now = new DateTime(2019, 9, 28);

            ModelBuilder.Entity<UserInfo>().HasData(new UserInfo { ID = 1, CreateTime = now, NickName = "对手1", Name = "对手1", PlayNumber = "1", City = "武汉", Country = "中国", Gender = Gender.Man, Phone = "13900000000", Province = "湖北" });

        }
    }
}
