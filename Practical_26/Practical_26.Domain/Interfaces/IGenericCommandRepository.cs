using Practical_26.Domain.Entities;

namespace Practical_26.Domain.Interfaces;

public interface IGenericCommandRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}
