using MediatR;
using Practical_25.Application.Command;
using Practical_25.Domain.Interfaces;

namespace Practical_25.Application.handlers;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, string>
{
    private readonly IUnitOfWork _uow;
    public DeleteEmployeeHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<string> Handle(DeleteEmployeeCommand command,CancellationToken token)
    {
        var employee = await _uow.EmployeeQuery.GetByIdAsync(command.Id);

        if (employee == null)
            throw new Exception("Not Found");
        
        _uow.EmployeeCommand.Delete(employee);
        
        await _uow.SaveChangesAsync();
        
        return "Employee Deactivated";
    }
}
