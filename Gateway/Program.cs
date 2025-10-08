using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") 
                 ?? "http://mana-food-api-service:8080";
var lambdaUrl = Environment.GetEnvironmentVariable("LAMBDA_URL") 
                ?? throw new InvalidOperationException("LAMBDA_URL é obrigatória");

Console.WriteLine($"🔗 API Base URL: {apiBaseUrl}");
Console.WriteLine($"⚡ Lambda URL: {lambdaUrl}");

// Configurar YARP dinamicamente
var routes = new[]
{
    new RouteConfig
    {
        RouteId = "apiRoute",
        ClusterId = "apiCluster", 
        Match = new RouteMatch { Path = "/manafood/{**catch-all}" },
        Transforms = new[]
        {
            new Dictionary<string, string> { { "PathRemovePrefix", "/manafood" } }
        }
    },
    new RouteConfig
    {
        RouteId = "lambdaRoute",
        ClusterId = "lambdaCluster",
        Match = new RouteMatch { Path = "/lambda/{**catch-all}" },
        Transforms = new[]
        {
            new Dictionary<string, string> { { "PathRemovePrefix", "/lambda" } }
        }
    }
};

var clusters = new[]
{
    new ClusterConfig
    {
        ClusterId = "apiCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "apiDestination", new DestinationConfig { Address = apiBaseUrl } }
        }
    },
    new ClusterConfig
    {
        ClusterId = "lambdaCluster", 
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "lambdaDestination", new DestinationConfig { Address = lambdaUrl } }
        }
    }
};

// Adicionar YARP configurado dinamicamente
builder.Services.AddSingleton<IProxyConfigProvider>(
    new InMemoryConfigProvider(routes, clusters));

builder.Services.AddReverseProxy();

var app = builder.Build();

app.MapReverseProxy();

Console.WriteLine("🚀 Gateway iniciado - configuração dinâmica aplicada");

app.Run();