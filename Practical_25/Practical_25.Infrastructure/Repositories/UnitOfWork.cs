using Practical_25.Domain.Entities;
using Practical_25.Domain.Interfaces;
using Practical_25.Infrastructure.Data;

namespace Practical_25.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericCommandRepository<Employee> EmployeeCommand { get; }
    public IGenericQueryRepository<Employee> EmployeeQuery { get; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        EmployeeCommand = new GenericCommandRepository<Employee>(_context);
        EmployeeQuery = new GenericQueryRepository<Employee>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
