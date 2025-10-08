using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;
using ManaFood.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

namespace ManaFood.Infrastructure.Configurations;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services)
    {
        var direct = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(direct))
        {
            Register(services, direct);
            return;
        }

        var secretArn = Environment.GetEnvironmentVariable("AURORA_SECRET_ARN");
        if (!string.IsNullOrWhiteSpace(secretArn))
        {
            var cs = BuildFromSecret(secretArn);
            Register(services, cs);
            return;
        }

        throw new InvalidOperationException(
            "Nenhuma forma de conexão encontrada. Defina CONNECTION_STRING ou AURORA_SECRET_ARN.");
    }

    private static void Register(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddSingleton<IPaymentProviderConfig, PaymentProviderConfig>();
    }

    private static string BuildFromSecret(string secretArn)
    {
        try
        {
            using var client = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.SAEast1);
            var response = client.GetSecretValueAsync(new GetSecretValueRequest
            {
                SecretId = secretArn
            }).GetAwaiter().GetResult();

            if (string.IsNullOrWhiteSpace(response.SecretString))
                throw new InvalidOperationException($"Secret {secretArn} retornou conteúdo vazio.");

            using var doc = JsonDocument.Parse(response.SecretString);
            var root = doc.RootElement;

            string? host = null;
            int port = 3306;
            string? username = null;
            string? password = null;
            string dbname = "manafooddb";


            if (root.TryGetProperty("host", out var hostEl))
            {
                host = hostEl.GetString();
            }
            
            
            if (string.IsNullOrWhiteSpace(host) && root.TryGetProperty("dbClusterIdentifier", out var clusterEl))
            {
                var clusterId = clusterEl.GetString();
                host = Environment.GetEnvironmentVariable("DATABASE_HOST");
                
                if (string.IsNullOrWhiteSpace(host))
                    throw new InvalidOperationException(
                        $"Secret não contém 'host' e DATABASE_HOST não está definida. ClusterID: {clusterId}");
            }

            if (string.IsNullOrWhiteSpace(host))
                throw new InvalidOperationException("Não foi possível determinar o host do banco de dados.");

            // Port
            if (root.TryGetProperty("port", out var portEl))
            {
                port = portEl.GetInt32();
            }

            // Username (obrigatório)
            if (root.TryGetProperty("username", out var userEl))
            {
                username = userEl.GetString();
            }
            else
            {
                throw new InvalidOperationException("Secret não contém 'username'.");
            }

            // Password (obrigatório)
            if (root.TryGetProperty("password", out var passEl))
            {
                password = passEl.GetString();
            }
            else
            {
                throw new InvalidOperationException("Secret não contém 'password'.");
            }

            // Database name
            if (root.TryGetProperty("dbname", out var dbnameEl))
            {
                dbname = dbnameEl.GetString() ?? "manafooddb";
            }

            return $"Server={host};Port={port};Database={dbname};User={username};Password={password};SslMode=Preferred;";
        }
        catch (AmazonSecretsManagerException ex)
        {
            throw new InvalidOperationException($"Erro ao acessar Secrets Manager: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Erro ao parsear JSON do secret: {ex.Message}", ex);
        }
    }
}