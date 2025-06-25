using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using ManaFood.Infrastructure.Database.Context;

namespace ManaFood.Infrastructure.Database.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(ApplicationContext applicationContext) : base(applicationContext) { }
}
