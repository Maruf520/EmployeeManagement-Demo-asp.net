using Microsoft.EntityFrameworkCore.Migrations;

namespace DEMO.Migrations
{
    public partial class EmployeeTableAltered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DesignationId1",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "DesignationId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "DesignationId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DesignationId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId1",
                table: "Employees",
                column: "DesignationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationId1",
                table: "Employees",
                column: "DesignationId1",
                principalTable: "Designations",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
