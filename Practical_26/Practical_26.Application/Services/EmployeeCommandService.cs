using AutoMapper;
using Practical_26.Application.Command;
using Practical_26.Application.DTOs;
using Practical_26.Application.Interfaces;
using Practical_26.Domain.Interfaces;
using Practical_26.Domain.Entities;

namespace Practical_26.Application.Services;

public class EmployeeCommandService : IEmployeeCommandService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EmployeeCommandService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeCommand emp)
    {
        var employee = _mapper.Map<Employee>(emp);

        await _uow.EmployeeCommand.AddAsync(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeCommand emp)
    {
        var employee = await _uow.EmployeeQuery.GetByIdAsync(emp.Id);

        if(employee == null)
        {
            throw new Exception("Employee Not Found");
        }

        _mapper.Map(emp, employee);

        _uow.EmployeeCommand.Update(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        var employee = await _uow.EmployeeQuery.GetByIdAsync(id);

        if(employee == null)
        {
            return false;
        }

        employee.Status = false;

        _uow.EmployeeCommand.Update(employee);

        await _uow.SaveChangesAsync();

        return true;
    }
}
