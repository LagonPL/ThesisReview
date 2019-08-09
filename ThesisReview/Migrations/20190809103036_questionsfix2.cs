using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisReview.Migrations
{
    public partial class questionsfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormURL",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FormURL",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
