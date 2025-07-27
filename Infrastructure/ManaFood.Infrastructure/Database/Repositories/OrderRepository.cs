using System.Linq.Expressions;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ManaFood.Infrastructure.Database.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    
    public async Task<List<Order>> GetAllActive(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _applicationContext.Set<Order>()
            .Where(x => !x.Deleted)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }
}
