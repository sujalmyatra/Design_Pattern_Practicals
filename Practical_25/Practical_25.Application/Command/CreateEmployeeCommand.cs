using MediatR;

namespace Practical_25.Application.Command;

public class CreateEmployeeCommand : IRequest<string>
{
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public string EmailId { get; set; } = string.Empty;
}
