using Microsoft.EntityFrameworkCore.Migrations;

namespace JdCat.Basketball.Model.Migrations
{
    public partial class Modify_UserInfo_PlayNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayNumber",
                table: "UserInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayNumber",
                table: "UserInfo");
        }
    }
}
