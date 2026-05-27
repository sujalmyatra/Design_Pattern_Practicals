using Practical_22.Domain.Entities;
using Practical_22.Domain.Interfaces;
using Practical_22.Infrastructure.Data;

namespace Practical_22.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        { }
    }
}
