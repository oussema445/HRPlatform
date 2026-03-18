using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Leaves",
                columns: new[] { "Id", "EmployeeId", "EmployeeName", "EndDate", "Reason", "StartDate", "Status", "TotalDays", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Ahmed Al-Rashid", new DateTime(2026, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Family vacation", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 7, "Annual" },
                    { 2, 2, "Sara Al-Qahtani", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medical", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", 3, "Sick" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaves");
        }
    }
}
