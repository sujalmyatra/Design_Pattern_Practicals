using Practical_25.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Practical_25.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasQueryFilter(e => e.Status);

        modelBuilder.Entity<Employee>().Property(e => e.Salary).HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}
