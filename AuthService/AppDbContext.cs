using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data — utilisateurs par défaut
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = "admin123", Role = "Admin", FullName = "Admin User", Email = "admin@hrplatform.com" },
            new User { Id = 2, Username = "hr", PasswordHash = "hr123", Role = "HR", FullName = "HR Manager", Email = "hr@hrplatform.com" },
            new User { Id = 3, Username = "employee", PasswordHash = "emp123", Role = "Employee", FullName = "John Employee", Email = "employee@hrplatform.com" }
        );
    }
}