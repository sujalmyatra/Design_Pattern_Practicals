using Practical_25.Application.DTOs;

namespace Practical_25.Application.Interfaces;

public interface IEmployeeQueryService
{
    Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id);
}
