namespace Practical_23.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool status { get; set; } = true;
}
