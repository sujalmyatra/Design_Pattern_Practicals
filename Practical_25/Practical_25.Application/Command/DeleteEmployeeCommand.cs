using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practical_25.Application.Command;

public class DeleteEmployeeCommand : IRequest<string>
{
    public Guid Id { get; set; }
}
