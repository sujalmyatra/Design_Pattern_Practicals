namespace Practical_24.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool status { get; set; } = true;
}
