using Practical_23.Domain.Entities;

namespace Practical_23.Application.Interfaces;

public interface IUnitOfWork
{   
    public IGenericRepository<Employee> Employees { get; }
    Task<int> SaveChangesAsync();
}
