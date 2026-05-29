using Microsoft.EntityFrameworkCore;
using Practical_25.Domain.Entities;
using Practical_25.Domain.Interfaces;
using Practical_25.Infrastructure.Data;

namespace Practical_25.Infrastructure.Repositories;

public class GenericQueryRepository<T> : IGenericQueryRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericQueryRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);

    }
}
