using System.Linq.Expressions;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetApprovedOrdersAsync(CancellationToken cancellationToken);
    Task<List<Order>> GetAllActive(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken);
    Task<Order?> GetByIdWithProductsAsync(Guid id);
    Task UpdateAsync(Order order);
}