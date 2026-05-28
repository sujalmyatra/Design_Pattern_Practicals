using Practical_22.Application.Interfaces;
using Practical_22.Application.DTOs;
using Practical_22.Domain.Interfaces;
using Practical_22.Domain.Entities;
using AutoMapper;

namespace Practical_22.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public EmployeeService(IUnitOfWork uow, IMapper mapper, ILoggerService logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            _logger.Log("Create Employee Started");

            var employee = _mapper.Map<Employee>(dto);

            await _uow.Employees.AddAsync(employee);

            await _uow.SaveChangesAsync();

            _logger.Log($"Employee Created : {employee.Name}");

            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            _logger.Log($"Update Employee Started : {dto.Id}");

            var employee = await _uow.Employees.GetByIdAsync(dto.Id);

            if(employee == null)
            {
                _logger.Log($"Employee Not Found : {dto.Id}");

                throw new Exception("Employee Not Found");
            }

            _mapper.Map(dto, employee);

            _uow.Employees.Update(employee);

            await _uow.SaveChangesAsync();

            _logger.Log($"Employee Updated Successfully : {employee.Name}");

            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            _logger.Log($"Delete Employee Started : {id}");

            var employee = await _uow.Employees.GetByIdAsync(id);

            if (employee == null)
            {
                _logger.Log($"Employee Not Found For Delete : {id}");

                return false;
            }

            employee.status = false;

            _uow.Employees.Update(employee);

            await _uow.SaveChangesAsync();

            _logger.Log($"Employee Deleted Successfully : {employee.Name}");

            return true;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id)
        {
            _logger.Log("Get Employee Method Called");

            if (id.HasValue)
            {
                _logger.Log($"Fetching Employee By Id : {id}");

                var employee = await _uow.Employees.GetByIdAsync(id.Value);

                if(employee == null)
                {
                    _logger.Log($"Employee Not Found : {id}");

                    return new List<EmployeeResponseDto>();
                }

                _logger.Log($"Employee Found : {employee.Name}");

                return new List<EmployeeResponseDto>
                {
                    _mapper.Map<EmployeeResponseDto>(employee)
                };
            }

            _logger.Log("Fetching All Employees");

            var employees = await _uow.Employees.GetAllAsync();

            _logger.Log($"Total Employees Found : {employees.Count()}");

            return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
        }
    }
}
