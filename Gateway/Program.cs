using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Variáveis de ambiente injetadas via ConfigMap/Secret
var apiBase = Environment.GetEnvironmentVariable("API_BASE_URL");   // ex: http://mana-food-api-service:8080
var lambdaUrl = Environment.GetEnvironmentVariable("LAMBDA_URL");   // ex: https://xxxx.execute-api.us-east-1.amazonaws.com/prod

// Rotas estáticas (independentes dos destinos)
var routes = new[]
{
    new RouteConfig
    {
        RouteId = "apiRoute",
        ClusterId = "apiCluster",
        Match = new RouteMatch { Path = "/manafood/{**catch-all}" },
        Transforms = new[]
        {
            new Dictionary<string,string> { { "PathRemovePrefix", "/manafood" } }
        }
    },
    new RouteConfig
    {
        RouteId = "lambdaRoute",
        ClusterId = "lambdaCluster",
        Match = new RouteMatch { Path = "/lambda/{**catch-all}" },
        Transforms = new[]
        {
            new Dictionary<string,string> { { "PathRemovePrefix", "/lambda" } }
        }
    }
};

var clusters = new List<ClusterConfig>();

if (!string.IsNullOrWhiteSpace(apiBase))
{
    clusters.Add(new ClusterConfig
    {
        ClusterId = "apiCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            {
                "apiDestination",
                new DestinationConfig
                {
                    Address = apiBase.EndsWith('/') ? apiBase : apiBase + "/"
                }
            }
        }
    });
}

if (!string.IsNullOrWhiteSpace(lambdaUrl))
{
    clusters.Add(new ClusterConfig
    {
        ClusterId = "lambdaCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            {
                "lambdaDestination",
                new DestinationConfig
                {
                    Address = lambdaUrl.EndsWith('/') ? lambdaUrl : lambdaUrl + "/"
                }
            }
        }
    });
}

// Registra YARP com configuração em memória (dinâmica via env vars)
builder.Services
    .AddReverseProxy()
    .LoadFromMemory(routes, clusters);

var app = builder.Build();

// Endpoint de health para probes
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));

// Reverse proxy
app.MapReverseProxy();

app.Run();