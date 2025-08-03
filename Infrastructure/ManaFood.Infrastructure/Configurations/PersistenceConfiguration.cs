using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ManaFood.Infrastructure.Configurations
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            // Registra repositório da Order para injeção de dependência
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Padrão para futuros repositórios:
            // services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
