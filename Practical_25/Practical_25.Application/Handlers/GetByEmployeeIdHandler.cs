using AutoMapper;
using MediatR;
using Practical_25.Application.DTOs;
using Practical_25.Application.Query;
using Practical_25.Domain.Interfaces;

namespace Practical_25.Application.Handlers;

public class GetByIdHandler : IRequestHandler<GetByEmployeeIdQuery, EmployeeResponseDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByIdHandler(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<EmployeeResponseDto> Handle(GetByEmployeeIdQuery query,CancellationToken token)
    {
        var employee = await _uow.EmployeeQuery.GetByIdAsync(query.Id);

        return _mapper.Map<EmployeeResponseDto>(employee);
    }
}
