using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Payroll> Payrolls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payroll>().HasData(
            new Payroll { Id = 1, EmployeeId = 1, EmployeeName = "Ahmed Al-Rashid", Month = 3, Year = 2026, BasicSalary = 12000, HousingAllowance = 2400, TransportAllowance = 800, Bonus = 500, Deductions = 0, NetSalary = 15700, Status = "Paid" },
            new Payroll { Id = 2, EmployeeId = 2, EmployeeName = "Sara Al-Qahtani", Month = 3, Year = 2026, BasicSalary = 15000, HousingAllowance = 3000, TransportAllowance = 800, Bonus = 1000, Deductions = 0, NetSalary = 19800, Status = "Paid" }
        );
    }
}