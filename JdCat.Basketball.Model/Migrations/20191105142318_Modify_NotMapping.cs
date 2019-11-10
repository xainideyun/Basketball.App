using Microsoft.EntityFrameworkCore.Migrations;

namespace JdCat.Basketball.Model.Migrations
{
    public partial class Modify_NotMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_UserInfo_UserInfoId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Match_MatchId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_UserInfo_UserInfoId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Match_MatchId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Match_MatchId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_UserInfo_UserInfoId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_MatchId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_UserInfoId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Section_MatchId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Player_MatchId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TeamId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_UserInfoId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Match_UserInfoId",
                table: "Match");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Team_MatchId",
                table: "Team",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_UserInfoId",
                table: "Team",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_MatchId",
                table: "Section",
                column: "MatchId");

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
                name: "IX_Match_UserInfoId",
                table: "Match",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_UserInfo_UserInfoId",
                table: "Match",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Match_MatchId",
                table: "Player",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_UserInfo_UserInfoId",
                table: "Player",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Match_MatchId",
                table: "Section",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Match_MatchId",
                table: "Team",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_UserInfo_UserInfoId",
                table: "Team",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
