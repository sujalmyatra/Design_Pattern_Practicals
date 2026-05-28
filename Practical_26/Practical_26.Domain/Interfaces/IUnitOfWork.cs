using Practical_26.Domain.Entities;

namespace Practical_26.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericCommandRepository<Employee> EmployeeCommand { get; }
    IGenericQueryRepository<Employee> EmployeeQuery { get; }
    Task<int> SaveChangesAsync();
}
