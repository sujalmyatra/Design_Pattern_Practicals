using AutoMapper;
using MediatR;
using Practical_25.Domain.Entities;
using Practical_25.Application.Command;
using Practical_25.Domain.Interfaces;

namespace Practical_25.Application.handlers;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand,string>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateEmployeeHandler(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateEmployeeCommand command,CancellationToken token)
    {
        var employee = _mapper.Map<Employee>(command);

        if (employee == null)
            throw new Exception("Not Found");

        await _uow.EmployeeCommand.AddAsync(employee);

        await _uow.SaveChangesAsync();

        return "Employee Created";

    }
}   
