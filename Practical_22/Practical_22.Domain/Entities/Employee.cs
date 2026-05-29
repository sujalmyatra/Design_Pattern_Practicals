using Practical_22.Domain.Enums;

namespace Practical_22.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public Department DepartmentId { get; set; }
    public string EmailId { get; set; }
    public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
}
