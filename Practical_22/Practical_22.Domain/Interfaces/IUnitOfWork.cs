
using Practical_22.Domain.Entities;

namespace Practical_22.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }
        Task<int> SaveChangesAsync();
    }
}
