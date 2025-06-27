using System.Linq.Expressions;
using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ManaFood.Infrastructure.Database.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationContext _applicationContext;

    public BaseRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await _applicationContext.Set<T>().Where(x => !x.Deleted).ToListAsync(cancellationToken);
    }

    // public virtual async Task<T> GetById(Guid id, CancellationToken cancellationToken)
    // {
    //     return await _applicationContext.Set<T>()
    //     .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted, cancellationToken);
    // }
    public async Task<T> GetBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        var query = _applicationContext.Set<T>().AsQueryable();

        if (includes?.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<T>> GetByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _applicationContext.Set<T>()
            .Where(x => ids.Contains(x.Id) && !x.Deleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<T> Create(T entity, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        entity.Id = Guid.NewGuid();
        entity.CreatedAt = now;
        entity.UpdatedAt = now;
        _applicationContext.Add(entity);
        return entity;
    }

    public async Task<T> Update(T entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _applicationContext.Update(entity);
        return entity;
    }

    public async Task Delete(T entity, CancellationToken cancellationToken)
    {
        entity.Deleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _applicationContext.Update(entity);
    }

}
