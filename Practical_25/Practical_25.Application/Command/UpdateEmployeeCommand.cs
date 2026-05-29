using MediatR;

namespace Practical_25.Application.Command;

public class UpdateEmployeeCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public string EmailId { get; set; } = String.Empty;
}
