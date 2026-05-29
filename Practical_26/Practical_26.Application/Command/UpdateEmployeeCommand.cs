namespace Practical_26.Application.Command;

public class UpdateEmployeeCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }
    public string EmailId { get; set; } = String.Empty;
}
