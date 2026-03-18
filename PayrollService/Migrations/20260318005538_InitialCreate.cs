using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BasicSalary = table.Column<double>(type: "float", nullable: false),
                    HousingAllowance = table.Column<double>(type: "float", nullable: false),
                    TransportAllowance = table.Column<double>(type: "float", nullable: false),
                    Bonus = table.Column<double>(type: "float", nullable: false),
                    Deductions = table.Column<double>(type: "float", nullable: false),
                    NetSalary = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Payrolls",
                columns: new[] { "Id", "BasicSalary", "Bonus", "Deductions", "EmployeeId", "EmployeeName", "HousingAllowance", "Month", "NetSalary", "Status", "TransportAllowance", "Year" },
                values: new object[,]
                {
                    { 1, 12000.0, 500.0, 0.0, 1, "Ahmed Al-Rashid", 2400.0, 3, 15700.0, "Paid", 800.0, 2026 },
                    { 2, 15000.0, 1000.0, 0.0, 2, "Sara Al-Qahtani", 3000.0, 3, 19800.0, "Paid", 800.0, 2026 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payrolls");
        }
    }
}
