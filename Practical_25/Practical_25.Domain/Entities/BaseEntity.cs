namespace Practical_25.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool Status { get; set; } = true;
}