using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JdCat.Basketball.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Log",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        CreateTime = table.Column<DateTime>(nullable: true),
            //        Application = table.Column<string>(nullable: true),
            //        Level = table.Column<string>(nullable: true),
            //        Message = table.Column<string>(nullable: true),
            //        Logger = table.Column<string>(nullable: true),
            //        Callsite = table.Column<string>(nullable: true),
            //        Exception = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Log", x => x.ID);
            //    });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlayNumber = table.Column<string>(nullable: true),
                    FaceUrl = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ActivityEnroll",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ActivityTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    JoinQuantity = table.Column<int>(nullable: false),
                    AbsentQuantity = table.Column<int>(nullable: false),
                    PendingQuantity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityEnroll", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivityEnroll_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    TakeupTime = table.Column<long>(nullable: false),
                    HostScore = table.Column<double>(nullable: false),
                    HostName = table.Column<string>(nullable: true),
                    VisitorScore = table.Column<double>(nullable: false),
                    VisitorName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    ContinueTime = table.Column<long>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Match_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityParticipant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    JoinTime = table.Column<DateTime>(nullable: false),
                    FaceUrl = table.Column<string>(nullable: true),
                    PlayNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false),
                    ActivityEnrollId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityParticipant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivityParticipant_ActivityEnroll_ActivityEnrollId",
                        column: x => x.ActivityEnrollId,
                        principalTable: "ActivityEnroll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityParticipant_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LogTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MatchLog_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchLog_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    PartNumber = table.Column<int>(nullable: false),
                    HostScore = table.Column<int>(nullable: false),
                    VisitorScore = table.Column<int>(nullable: false),
                    TakeupTime = table.Column<long>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    PauseTime = table.Column<long>(nullable: false),
                    ContinueTime = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Section_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    ThreePoint = table.Column<int>(nullable: false),
                    UnThreePoint = table.Column<int>(nullable: false),
                    TwoPoint = table.Column<int>(nullable: false),
                    UnTwoPoint = table.Column<int>(nullable: false),
                    OnePoint = table.Column<int>(nullable: false),
                    UnOnePoint = table.Column<int>(nullable: false),
                    Foul = table.Column<int>(nullable: false),
                    Backboard = table.Column<int>(nullable: false),
                    BlockShot = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    Steals = table.Column<int>(nullable: false),
                    Fault = table.Column<int>(nullable: false),
                    Suspend = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Team_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlayNumber = table.Column<string>(nullable: true),
                    FaceUrl = table.Column<string>(nullable: true),
                    TakeupTime = table.Column<long>(nullable: false),
                    ContinueTime = table.Column<long>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    ThreePoint = table.Column<int>(nullable: false),
                    UnThreePoint = table.Column<int>(nullable: false),
                    TwoPoint = table.Column<int>(nullable: false),
                    UnTwoPoint = table.Column<int>(nullable: false),
                    OnePoint = table.Column<int>(nullable: false),
                    UnOnePoint = table.Column<int>(nullable: false),
                    Foul = table.Column<int>(nullable: false),
                    Backboard = table.Column<int>(nullable: false),
                    BlockShot = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    Steals = table.Column<int>(nullable: false),
                    Fault = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Player_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "ID", "City", "Country", "CreateTime", "FaceUrl", "Gender", "Name", "NickName", "OpenId", "Phone", "PlayNumber", "Province" },
                values: new object[] { 1, "武汉", "中国", new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "对手1", "对手1", null, "13900000000", "1", "湖北" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEnroll_UserInfoId",
                table: "ActivityEnroll",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityParticipant_ActivityEnrollId",
                table: "ActivityParticipant",
                column: "ActivityEnrollId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityParticipant_UserInfoId",
                table: "ActivityParticipant",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_UserInfoId",
                table: "Match",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchLog_MatchId",
                table: "MatchLog",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchLog_UserInfoId",
                table: "MatchLog",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_MatchId",
                table: "Player",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserInfoId",
                table: "Player",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_MatchId",
                table: "Section",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_MatchId",
                table: "Team",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_UserInfoId",
                table: "Team",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityParticipant");

            //migrationBuilder.DropTable(
            //    name: "Log");

            migrationBuilder.DropTable(
                name: "MatchLog");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "ActivityEnroll");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
