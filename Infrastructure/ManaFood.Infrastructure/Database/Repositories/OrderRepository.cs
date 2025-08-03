using System.Linq.Expressions;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
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
                .Where(o => o.OrderStatus == OrderStatus.RECEIVED)
                .OrderBy(o => o.UpdatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAllActive(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<Order>()
                .Where(x => !x.Deleted)
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdWithProductsAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

    }
}
