using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisReview.Migrations
{
    public partial class userlistlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_ApplicationUserId",
                table: "UserLists",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLists_AspNetUsers_ApplicationUserId",
                table: "UserLists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLists_AspNetUsers_ApplicationUserId",
                table: "UserLists");

            migrationBuilder.DropIndex(
                name: "IX_UserLists_ApplicationUserId",
                table: "UserLists");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserLists");
        }
    }
}
