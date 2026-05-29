using Practical_23.Application.DTOs;

namespace Practical_23.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto);
    Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto);
    Task<bool> DeleteEmployeeAsync(Guid id);
    Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id);
    Task<decimal> GetOverTimePayAsync(OvertimeRequestDto dto);

}
