namespace Practical_23.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Status { get; set; }
}
