using ManaFood.Domain.Entities;
using ManaFood.Domain.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ManaFood.Infrastructure.Database.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext applicationContext) : base(applicationContext) { }
}
