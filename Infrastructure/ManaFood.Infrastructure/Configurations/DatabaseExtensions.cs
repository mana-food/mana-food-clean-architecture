using ManaFood.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ManaFood.Infrastructure.Configurations;

public static class DatabaseExtensions
{
    public static async Task<IHost> MigrateDbContextAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<ApplicationContext>>();
        var context = services.GetRequiredService<ApplicationContext>();

        try
        {            
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Aplicando {Count} migration(s): {Migrations}", 
                    pendingMigrations.Count(), 
                    string.Join(", ", pendingMigrations));
                
                await context.Database.MigrateAsync();
                logger.LogInformation("Migrations aplicadas com sucesso!");
            }
            else
            {
                logger.LogInformation("Banco de dados está atualizado.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao configurar banco de dados");
            throw;
        }

        return host;
    }
}