
using Practical_24.Domain.Entities;

namespace Practical_24.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }
        Task<int> SaveChangesAsync();
    }
}
