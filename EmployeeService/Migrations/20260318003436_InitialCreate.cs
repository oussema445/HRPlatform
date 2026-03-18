using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "FullName", "HireDate", "Phone", "Position", "Salary", "Status" },
                values: new object[,]
                {
                    { 1, "IT", "ahmed@hrplatform.com", "Ahmed Al-Rashid", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "+966501234567", "Software Engineer", 12000.0, "Active" },
                    { 2, "HR", "sara@hrplatform.com", "Sara Al-Qahtani", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "+966507654321", "HR Manager", 15000.0, "Active" },
                    { 3, "Finance", "mohammed@hrplatform.com", "Mohammed Al-Zahrani", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+966509876543", "Accountant", 10000.0, "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
