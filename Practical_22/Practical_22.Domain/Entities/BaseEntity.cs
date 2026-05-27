namespace Practical_22.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool status { get; set; } = true;
    }
}
