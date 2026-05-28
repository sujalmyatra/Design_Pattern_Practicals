using Practical_26.Domain.Entities;

namespace Practical_26.Domain.Interfaces;

public interface IGenericQueryRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);
}
