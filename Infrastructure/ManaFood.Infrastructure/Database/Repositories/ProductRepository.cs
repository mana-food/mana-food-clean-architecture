using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ManaFood.Infrastructure.Database.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext applicationContext) : base(applicationContext) { }
}
