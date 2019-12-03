using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisReview.Migrations
{
    public partial class formlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "Reports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FormId",
                table: "Reports",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Forms_FormId",
                table: "Reports",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "FormId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Forms_FormId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_FormId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Reports");
        }
    }
}
