namespace Practical_26.Application.Command;

public class CreateEmployeeCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public string EmailId { get; set; } = string.Empty;
}
