using Microsoft.EntityFrameworkCore;
using Practical_23.Application.Interfaces;
using Practical_23.Domain.Entities;
using Practical_23.DAL.Data;

namespace Practical_23.DAL.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _set;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync() {
        return await _set.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _set.FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        await _set.AddAsync(entity);
    }
    public void Update(T entity)
    {
         _set.Update(entity);
    }
    public void Delete(T entity)
    {
        entity.Status = false;
        _set.Update(entity);
    }
}
    