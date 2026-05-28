using AutoMapper;
using Practical_23.Application.DTOs;
using Practical_23.Application.Interfaces;
using Practical_23.Domain.Entities;

namespace Practical_23.Application.Service;

public class EmpService : IEmpService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public EmpService(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ResponseDto>> GetAllAsync()
    {
        var employees = await _uow.Employees.GetAllAsync();
        return _mapper.Map<IEnumerable<ResponseDto>>(employees);
    }

    public async Task<ResponseDto> GetByIdAsync(Guid id)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);
        return _mapper.Map<ResponseDto>(employee);
    }
    public async Task AddAsync(CreateDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);

        if (employee == null)
            throw new Exception("Employee not found");

        employee.Joiningdate = DateTime.Now;

        employee.Status = true;

        await _uow.Employees.AddAsync(employee);
        await _uow.SaveChangesAsync();
    }
    public async Task UpdateAsync (UpdateDto dto)
    {
        var employee = await _uow.Employees.GetByIdAsync(dto.Id);

        if (employee == null)
            throw new Exception("Employee not found");

        _mapper.Map(dto, employee);
        await _uow.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);

        if (employee == null)
            throw new Exception("Employee not found");

        _uow.Employees.Delete(employee);

        await _uow.SaveChangesAsync();
    }
}
