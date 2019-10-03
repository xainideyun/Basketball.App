﻿using JdCat.Basketball.Common;
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

            ModelBuilder.Entity<UserInfo>().HasData(new UserInfo { ID = 1, FaceUrl = "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJfMTu7XeEEA2ZmMOxuowJRf4BXGOXXqZtj6ZTCLeU4cTTsFicMUk8BPW2icVIZwQpfowBotjibAHMFg/132", CreateTime = now, NickName = "华天晓", City = "武汉", Country = "中国", Gender = Gender.Man, Phone = "17354300837", Province = "湖北" });

        }
    }
}
