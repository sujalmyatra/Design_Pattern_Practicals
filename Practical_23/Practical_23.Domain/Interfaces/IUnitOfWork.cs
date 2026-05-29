
using Practical_23.Domain.Entities;

namespace Practical_23.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Employee> Employees { get; }
    Task<int> SaveChangesAsync();
}
