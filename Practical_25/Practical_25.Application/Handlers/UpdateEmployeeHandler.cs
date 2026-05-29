using AutoMapper;
using MediatR;
using Practical_25.Application.Command;
using Practical_25.Domain.Interfaces;

namespace Practical_25.Application.handlers;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand ,string>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UpdateEmployeeHandler(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<string> Handle(UpdateEmployeeCommand command,CancellationToken token)
    {
        var employee = await _uow.EmployeeQuery.GetByIdAsync(command.Id);

        if (employee == null)
            throw new Exception("Not Found");

        _mapper.Map(command, employee);

        _uow.EmployeeCommand.Update(employee);

        await  _uow.SaveChangesAsync();

        return "Employee Update";
    }
}
