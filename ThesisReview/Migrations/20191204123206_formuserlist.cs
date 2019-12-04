using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisReview.Migrations
{
    public partial class formuserlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuardianUserUserListId",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerUserUserListId",
                table: "Forms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_GuardianUserUserListId",
                table: "Forms",
                column: "GuardianUserUserListId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_ReviewerUserUserListId",
                table: "Forms",
                column: "ReviewerUserUserListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_UserLists_GuardianUserUserListId",
                table: "Forms",
                column: "GuardianUserUserListId",
                principalTable: "UserLists",
                principalColumn: "UserListId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_UserLists_ReviewerUserUserListId",
                table: "Forms",
                column: "ReviewerUserUserListId",
                principalTable: "UserLists",
                principalColumn: "UserListId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_UserLists_GuardianUserUserListId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_UserLists_ReviewerUserUserListId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_GuardianUserUserListId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_ReviewerUserUserListId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "GuardianUserUserListId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "ReviewerUserUserListId",
                table: "Forms");
        }
    }
}
