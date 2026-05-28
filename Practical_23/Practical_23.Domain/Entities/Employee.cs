using Practical_23.Domain.Enums;

namespace Practical_23.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public Deparment DepartmentId { get; set; }
    public string EmailId { get; set; } = string.Empty;
    public DateTime Joiningdate { get; set; }

}
