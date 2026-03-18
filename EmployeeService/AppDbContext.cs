using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, FullName = "Ahmed Al-Rashid", Email = "ahmed@hrplatform.com", Phone = "+966501234567", Position = "Software Engineer", Department = "IT", HireDate = new DateTime(2022, 1, 15), Salary = 12000, Status = "Active" },
            new Employee { Id = 2, FullName = "Sara Al-Qahtani", Email = "sara@hrplatform.com", Phone = "+966507654321", Position = "HR Manager", Department = "HR", HireDate = new DateTime(2021, 3, 10), Salary = 15000, Status = "Active" },
            new Employee { Id = 3, FullName = "Mohammed Al-Zahrani", Email = "mohammed@hrplatform.com", Phone = "+966509876543", Position = "Accountant", Department = "Finance", HireDate = new DateTime(2023, 6, 1), Salary = 10000, Status = "Active" }
        );
    }
}