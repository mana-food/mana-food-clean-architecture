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
        return await _applicationContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<T> Create(T entity, CancellationToken cancellationToken)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _applicationContext.Add(entity);
        return entity;
    }

    public async Task<T> Update(T entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _applicationContext.Update(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        entity.Deleted = true;
        _applicationContext.Remove(entity);
    }  

}
