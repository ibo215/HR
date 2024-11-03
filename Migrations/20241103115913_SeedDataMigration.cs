using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName", "IsActive" },
                values: new object[,]
                {
                    { 1, "HR", true },
                    { 2, "IT", true },
                    { 3, "Finance", true }
                });

            migrationBuilder.InsertData(
                table: "SalaryTiers",
                columns: new[] { "SalaryTierId", "IsActive", "SalaryAmount", "TierName" },
                values: new object[,]
                {
                    { 1, true, 3000m, "Junior" },
                    { 2, true, 5000m, "Mid-Level" },
                    { 3, true, 7000m, "Senior" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "IsActive", "Name", "Position", "SalaryTierId" },
                values: new object[,]
                {
                    { 1, 1, true, "Alice Smith", "HR Manager", 2 },
                    { 2, 2, true, "Bob Johnson", "Software Developer", 1 },
                    { 3, 3, true, "Charlie Brown", "Finance Analyst", 2 },
                    { 4, 2, true, "Diana Prince", "Senior Developer", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SalaryTiers",
                keyColumn: "SalaryTierId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SalaryTiers",
                keyColumn: "SalaryTierId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SalaryTiers",
                keyColumn: "SalaryTierId",
                keyValue: 3);
        }
    }
}
