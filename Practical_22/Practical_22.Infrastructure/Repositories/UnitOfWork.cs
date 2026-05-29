using Practical_22.Domain.Entities;
using Practical_22.Domain.Interfaces;
using Practical_22.Infrastructure.Data;

namespace Practical_22.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepository<Employee> Employees { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Employees = new GenericRepository<Employee>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
