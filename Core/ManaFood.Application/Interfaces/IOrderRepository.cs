using ManaFood.Domain.Entities;

namespace ManaFood.Application.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetApprovedOrdersAsync(CancellationToken cancellationToken);
}