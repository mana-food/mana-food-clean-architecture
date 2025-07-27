using System.Linq.Expressions;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> GetAllActive(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken);
}
