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
        var client = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.SAEast1);
        var response = client.GetSecretValueAsync(new GetSecretValueRequest
        {
            SecretId = secretArn
        }).GetAwaiter().GetResult();

        var doc = JsonDocument.Parse(response.SecretString);
        var host = doc.RootElement.GetProperty("host").GetString();
        var port = doc.RootElement.GetProperty("port").GetInt32();
        var user = doc.RootElement.GetProperty("username").GetString();
        var pass = doc.RootElement.GetProperty("password").GetString();
        var db = doc.RootElement.TryGetProperty("dbname", out var dbEl)
            ? dbEl.GetString()
            : "manafooddb";

        return $"Server={host};Port={port};Database={db};User={user};Password={pass};SslMode=Preferred;";
    }
}