using Practical_23.Domain.Enums;

namespace Practical_23.Application.DTOs;

public class CreateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public Deparment DepartmentId { get; set; }
    public string EmailId { get; set; } = string.Empty;
}
