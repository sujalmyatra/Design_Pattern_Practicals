using Practical_25.Domain.Entities;

namespace Practical_25.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericCommandRepository<Employee> EmployeeCommand { get; }
    IGenericQueryRepository<Employee> EmployeeQuery { get; }
    Task<int> SaveChangesAsync();
}
