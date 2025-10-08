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
        string? direct = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(direct))
        {
            Register(services, direct);
            return;
        }

        var secretArn = Environment.GetEnvironmentVariable("AURORA_SECRET_ARN");
        if (!string.IsNullOrWhiteSpace(secretArn))
        {
            try
            {
                var cs = BuildFromSecret(secretArn);
                Register(services, cs);
                return;
            }
            catch (Exception ex)
            {
                var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
                if (!string.IsNullOrWhiteSpace(host))
                {
                    var dbName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "manafooddb";
                    var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "3306";
                    var degraded = $"Server={host};Port={port};Database={dbName};User=invalid;Password=invalid;SslMode=Preferred;";
                    Register(services, degraded);
                    return;
                }
                throw; 
            }
        }

        throw new InvalidOperationException("Nenhuma forma de conexão encontrada. Defina CONNECTION_STRING ou AURORA_SECRET_ARN.");
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
        // Region dinâmica
        var regionName = Environment.GetEnvironmentVariable("AWS_REGION") ?? "us-east-1";
        var region = Amazon.RegionEndpoint.GetBySystemName(regionName);

        using var client = new AmazonSecretsManagerClient(region);
        GetSecretValueResponse response;
        try
        {
            response = client.GetSecretValueAsync(new GetSecretValueRequest
            {
                SecretId = secretArn
            }).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Falha ao obter secret {secretArn} na região {regionName}", ex);
        }

        if (string.IsNullOrWhiteSpace(response.SecretString))
            throw new InvalidOperationException("Secret retornou vazio.");

        using var doc = JsonDocument.Parse(response.SecretString);
        var root = doc.RootElement;

        string? Try(string p) => root.TryGetProperty(p, out var el) ? el.GetString() : null;

        var host = Try("host") ?? Environment.GetEnvironmentVariable("DATABASE_HOST")
            ?? throw new KeyNotFoundException("Campo 'host' ausente no secret e 'DATABASE_HOST' não definido.");
        var port = Try("port") ?? (Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "3306");
        var user = Try("username") ?? throw new KeyNotFoundException("Campo 'username' ausente no secret.");
        var pass = Try("password") ?? throw new KeyNotFoundException("Campo 'password' ausente no secret.");
        var db = Try("dbname") ?? (Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "manafooddb");

        return $"Server={host};Port={port};Database={db};User={user};Password={pass};SslMode=Preferred;";
    }
}