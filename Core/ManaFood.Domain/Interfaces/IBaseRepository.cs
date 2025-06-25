using ManaFood.Domain.Entities;

namespace ManaFood.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<T> GetById(Guid id, CancellationToken cancellationToken);
    Task<T> Create(T entity, CancellationToken cancellationToken);
    Task<T> Update(T entity, CancellationToken cancellationToken);
    Task Delete(T entity, CancellationToken cancellationToken);
}
