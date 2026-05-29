using Practical_25.Application.Command;
using Practical_25.Application.DTOs;

namespace Practical_25.Application.Interfaces;

public interface IEmployeeCommandService
{
    Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeCommand employee);
    Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeCommand employee);
    Task<bool> DeleteEmployeeAsync(Guid id);

}
