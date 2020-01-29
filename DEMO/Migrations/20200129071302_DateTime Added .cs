using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEMO.Migrations
{
    public partial class DateTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Salaries");

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Salaries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Salaries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Salaries");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
