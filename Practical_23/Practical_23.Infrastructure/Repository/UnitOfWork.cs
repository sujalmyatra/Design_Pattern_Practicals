using Practical_23.Application.Interfaces;
using Practical_23.Domain.Entities;
using Practical_23.DAL.Data;

namespace Practical_23.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepository<Employee> Employees { get; }
    public UnitOfWork(AppDbContext context,IGenericRepository<Employee> employees)
    {
        _context = context;
        Employees = employees;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

}
