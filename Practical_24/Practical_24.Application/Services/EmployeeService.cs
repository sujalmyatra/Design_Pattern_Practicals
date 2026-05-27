using Practical_24.Application.Interfaces;
using Practical_24.Application.DTOs;
using Practical_24.Domain.Interfaces;
using Practical_24.Domain.Entities;
using AutoMapper;

namespace Practical_24.Application.Services;

public class EmployeeService(IUnitOfWork uow, IMapper mapper, IEmployeeRepository repository) : IEmployeeService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;
    private readonly IEmployeeRepository _repository = repository;

    public async Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);

        await _repository.AddAsync(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(dto.Id);

        if(employee == null)
        {
            throw new Exception("Employee Not Found");
        }

        _mapper.Map(dto, employee);

        _repository.Update(employee);

        await _uow.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if(employee == null)
            return false;
        
        employee.status = false;

        _repository.Update(employee);

        await _uow.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id)
    {
        if(id.HasValue)
        {
            var employee = await _repository.GetByIdAsync(id.Value);

            if(employee == null)
            {
                return new List<EmployeeResponseDto>();
            }

            return new List<EmployeeResponseDto>
            {
                _mapper.Map<EmployeeResponseDto>(employee)
            };
        }

        var employees = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
    }
}
