using AutoMapper;
using MediatR;
using Practical_25.Application.DTOs;
using Practical_25.Application.Query;
using Practical_25.Domain.Interfaces;

namespace Practical_25.Application.Handlers;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeResponseDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllEmployeesHandler(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeResponseDto>> Handle(GetAllEmployeesQuery query,CancellationToken token)
    {
        var employees = await _uow.EmployeeQuery.GetAllAsync();

        return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
    }
}
