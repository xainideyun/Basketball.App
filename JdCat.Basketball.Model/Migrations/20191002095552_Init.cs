using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JdCat.Basketball.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Logger = table.Column<string>(nullable: true),
                    Callsite = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.ID);
                });

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

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "ID", "City", "Country", "CreateTime", "FaceUrl", "Gender", "Name", "NickName", "OpenId", "Phone", "Province" },
                values: new object[] { 1, "武汉", "中国", new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJfMTu7XeEEA2ZmMOxuowJRf4BXGOXXqZtj6ZTCLeU4cTTsFicMUk8BPW2icVIZwQpfowBotjibAHMFg/132", 1, null, "华天晓", null, "17354300837", "湖北" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityParticipant");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "ActivityEnroll");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
