using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisReview.Migrations
{
    public partial class datestartfinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Forms",
                newName: "DateTimeStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFinish",
                table: "Forms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeFinish",
                table: "Forms");

            migrationBuilder.RenameColumn(
                name: "DateTimeStart",
                table: "Forms",
                newName: "DateTime");
        }
    }
}
