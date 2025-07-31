using Microsoft.EntityFrameworkCore;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using ManaFood.Application.Shared;

namespace ManaFood.Infrastructure.Database.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<IEnumerable<Order>> GetApprovedOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Where(o => o.OrderStatus == OrderStatus.APROVADO)
                .OrderBy(o => o.UpdatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}