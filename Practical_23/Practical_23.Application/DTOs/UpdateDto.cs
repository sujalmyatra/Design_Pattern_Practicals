using Practical_23.Domain.Enums;

namespace Practical_23.Application.DTOs;

public class UpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public Deparment DepartmentId { get; set; }
    public string EmailId { get; set; } = string.Empty;
}
