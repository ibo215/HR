using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class edite_IsActive_to_InActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "SalaryTiers",
                newName: "InActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Employees",
                newName: "InActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Departments",
                newName: "InActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Accounts",
                newName: "InActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InActive",
                table: "SalaryTiers",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "InActive",
                table: "Employees",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "InActive",
                table: "Departments",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "InActive",
                table: "Accounts",
                newName: "IsActive");
        }
    }
}
