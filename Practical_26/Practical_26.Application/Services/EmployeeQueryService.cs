using AutoMapper;
using Practical_26.Application.DTOs;
using Practical_26.Application.Interfaces;
using Practical_26.Domain.Interfaces;


namespace Practical_26.Application.Services
{
    public class EmployeeQueryService : IEmployeeQueryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeeQueryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var employee = await _uow.EmployeeQuery.GetByIdAsync(id.Value);

                if(employee == null)
                {
                    return new List<EmployeeResponseDto>();
                }

                return new List<EmployeeResponseDto>
                {
                    _mapper.Map<EmployeeResponseDto>(employee)
                };
            }

            var employees = await _uow.EmployeeQuery.GetAllAsync();

            return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
        }
    }
}
