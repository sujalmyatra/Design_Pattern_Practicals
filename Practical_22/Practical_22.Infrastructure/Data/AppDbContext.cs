using Practical_22.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Practical_22.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasQueryFilter(e => e.status);

        modelBuilder.Entity<Employee>()
        .Property(e => e.Salary)
        .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}
