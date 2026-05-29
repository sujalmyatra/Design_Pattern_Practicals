using Microsoft.EntityFrameworkCore;
using Practical_25.Domain.Entities;
using Practical_25.Domain.Interfaces;
using Practical_25.Infrastructure.Data;

namespace Practical_25.Infrastructure.Repositories;

public class GenericCommandRepository<T> : IGenericCommandRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericCommandRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.Status = false;

        _context.Update(entity);
    }
}
