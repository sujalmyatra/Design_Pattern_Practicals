using Practical_26.Application.DTOs;

namespace Practical_26.Application.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id);
    }
}
