using Practical_24.Domain.Entities;
using Practical_24.Domain.Interfaces;
using Practical_24.Infrastructure.Data;

namespace Practical_24.Infrastructure.Repositories;

public class EmployeeRepository : GenericRepository<Employee>
{
    public EmployeeRepository(AppDbContext context) : base(context)
    { }
}
