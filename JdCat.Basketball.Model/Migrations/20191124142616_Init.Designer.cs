﻿// <auto-generated />
using System;
using JdCat.Basketball.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JdCat.Basketball.Model.Migrations
{
    [DbContext(typeof(BasketballDbContext))]
    [Migration("20191124142616_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.ActivityEnroll", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AbsentQuantity");

                    b.Property<DateTime>("ActivityTime");

                    b.Property<string>("Address");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("JoinQuantity");

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int>("PendingQuantity");

                    b.Property<int>("Quantity");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.HasIndex("UserInfoId");

                    b.ToTable("ActivityEnroll");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.ActivityParticipant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityEnrollId");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("FaceUrl");

                    b.Property<int>("Gender");

                    b.Property<DateTime>("JoinTime");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("PlayNumber");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.HasIndex("ActivityEnrollId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("ActivityParticipant");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Feedback", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Application");

                    b.Property<string>("Callsite");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("Exception");

                    b.Property<string>("Level");

                    b.Property<string>("Logger");

                    b.Property<string>("Message");

                    b.HasKey("ID");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Match", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<long>("ContinueTime");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<DateTime?>("EndTime");

                    b.Property<string>("HostName");

                    b.Property<double>("HostScore");

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.Property<string>("Location");

                    b.Property<int>("Mode");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int>("Status");

                    b.Property<long>("TakeupTime");

                    b.Property<int>("UserInfoId");

                    b.Property<string>("VisitorName");

                    b.Property<double>("VisitorScore");

                    b.HasKey("ID");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.MatchLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<DateTime>("LogTime");

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.HasIndex("MatchId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("MatchLog");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assists");

                    b.Property<int>("Backboard");

                    b.Property<int>("BlockShot");

                    b.Property<long>("ContinueTime");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("FaceUrl");

                    b.Property<int>("Fault");

                    b.Property<int>("Foul");

                    b.Property<int>("GetLost");

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.Property<int>("OnePoint");

                    b.Property<string>("PlayNumber");

                    b.Property<int>("Score");

                    b.Property<int>("Status");

                    b.Property<int>("Steals");

                    b.Property<long>("TakeupTime");

                    b.Property<int>("TeamId");

                    b.Property<int>("ThreePoint");

                    b.Property<int>("TwoPoint");

                    b.Property<int>("UnOnePoint");

                    b.Property<int>("UnThreePoint");

                    b.Property<int>("UnTwoPoint");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Section", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContinueTime");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<DateTime?>("EndTime");

                    b.Property<int>("HostScore");

                    b.Property<int>("MatchId");

                    b.Property<int>("PartNumber");

                    b.Property<long>("PauseTime");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int>("Status");

                    b.Property<long>("TakeupTime");

                    b.Property<int>("VisitorScore");

                    b.HasKey("ID");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assists");

                    b.Property<int>("Backboard");

                    b.Property<int>("BlockShot");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("Fault");

                    b.Property<int>("Foul");

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.Property<int>("OnePoint");

                    b.Property<int>("Score");

                    b.Property<int>("Steals");

                    b.Property<int>("Suspend");

                    b.Property<int>("ThreePoint");

                    b.Property<int>("TwoPoint");

                    b.Property<int>("UnOnePoint");

                    b.Property<int>("UnThreePoint");

                    b.Property<int>("UnTwoPoint");

                    b.Property<int>("UserInfoId");

                    b.HasKey("ID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.UserInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("FaceUrl");

                    b.Property<int>("Gender");

                    b.Property<bool>("HasMyselfProgram");

                    b.Property<string>("Name");

                    b.Property<string>("NickName");

                    b.Property<string>("OpenId");

                    b.Property<string>("Phone");

                    b.Property<string>("PlayNumber");

                    b.Property<string>("Province");

                    b.HasKey("ID");

                    b.ToTable("UserInfo");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            City = "武汉",
                            Country = "中国",
                            CreateTime = new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            HasMyselfProgram = false,
                            Name = "对手1",
                            NickName = "对手1",
                            Phone = "13900000000",
                            PlayNumber = "1",
                            Province = "湖北"
                        });
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.ActivityEnroll", b =>
                {
                    b.HasOne("JdCat.Basketball.Model.Domain.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.ActivityParticipant", b =>
                {
                    b.HasOne("JdCat.Basketball.Model.Domain.ActivityEnroll", "ActivityEnroll")
                        .WithMany("ActivityParticipants")
                        .HasForeignKey("ActivityEnrollId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JdCat.Basketball.Model.Domain.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.Feedback", b =>
                {
                    b.HasOne("JdCat.Basketball.Model.Domain.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JdCat.Basketball.Model.Domain.MatchLog", b =>
                {
                    b.HasOne("JdCat.Basketball.Model.Domain.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JdCat.Basketball.Model.Domain.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}