using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Leave> Leaves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Leave>().HasData(
            new Leave { Id = 1, EmployeeId = 1, EmployeeName = "Ahmed Al-Rashid", Type = "Annual", StartDate = new DateTime(2026, 4, 1), EndDate = new DateTime(2026, 4, 7), TotalDays = 7, Status = "Pending", Reason = "Family vacation" },
            new Leave { Id = 2, EmployeeId = 2, EmployeeName = "Sara Al-Qahtani", Type = "Sick", StartDate = new DateTime(2026, 3, 20), EndDate = new DateTime(2026, 3, 22), TotalDays = 3, Status = "Approved", Reason = "Medical" }
        );
    }
}