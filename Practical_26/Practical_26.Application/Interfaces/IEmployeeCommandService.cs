using Practical_26.Application.Command;
using Practical_26.Application.DTOs;

namespace Practical_26.Application.Interfaces;

public interface IEmployeeCommandService
{
    Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeCommand employee);
    Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeCommand employee);
    Task<bool> DeleteEmployeeAsync(Guid id);

}
