using Practical_22.Domain.Entities;

namespace Practical_22.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
