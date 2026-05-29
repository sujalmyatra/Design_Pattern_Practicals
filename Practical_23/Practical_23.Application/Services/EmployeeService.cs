using AutoMapper;
using Practical_23.Application.Abstract_Factory;
using Practical_23.Application.DTOs;
using Practical_23.Application.Interfaces;
using Practical_23.Domain.Entities;
using Practical_23.Domain.Interfaces;

namespace Practical_23.Application.Services;

public class EmployeeService(IUnitOfWork uow, IMapper mapper, AbstractFactoryProvider provider) : IEmployeeService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;
    private readonly AbstractFactoryProvider _provider = provider;

    public async Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);

        await _uow.Employees.AddAsync(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto)
    {
        var employee = await _uow.Employees.GetByIdAsync(dto.Id);

        if(employee == null)
        {
            throw new Exception("Employee Not Found");
        }

        _mapper.Map(dto, employee);

        _uow.Employees.Update(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);

        if(employee == null)
            return false;
        
        employee.status = false;

        _uow.Employees.Update(employee);

        await _uow.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id)
    {
        if(id.HasValue)
        {
            var employee = await _uow.Employees.GetByIdAsync(id.Value);

            if(employee == null)
            {
                return new List<EmployeeResponseDto>();
            }

            return new List<EmployeeResponseDto>
            {
                _mapper.Map<EmployeeResponseDto>(employee)
            };
        }

        var employees = await _uow.Employees.GetAllAsync();

        return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
    }

    public async Task<decimal> GetOverTimePayAsync(OvertimeRequestDto dto)
    {
        var employee = await _uow.Employees.GetByIdAsync(dto.Id);

        if (employee == null)
            throw new Exception("Not Found");

        var abstractFactory = _provider.GetFactory(employee.DepartmentId);

        var factory = abstractFactory.GetFactory(employee.DepartmentId);

        var overTimePay = factory.Create();

        return overTimePay.CalcultateOverTimePay(dto.Hours);
    }
}
