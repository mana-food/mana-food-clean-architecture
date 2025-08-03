using ManaFood.Infrastructure.Configurations;
using ManaFood.Application.Configurations;
using ManaFood.Infrastructure.Services;
using ManaFood.Infrastructure.Services.MercadoPago;

using ManaFood.Application.Interfaces;
using ManaFood.Application.Interfaces.Services;
using ManaFood.Application.Services;

using ManaFood.Infrastructure.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");

// JWT & Auth services
builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
builder.Services.AddSingleton<IJwtService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var jwtSettings = configuration.GetSection("Jwt");
    var secretKey = jwtSettings["SecretKey"];
    var expiration = int.Parse(jwtSettings["ExpirationMinutes"]);
    var issuer = jwtSettings["Issuer"];
    var audience = jwtSettings["Audience"];
    var blacklistService = provider.GetRequiredService<ITokenBlacklistService>();
    return new JwtService(secretKey, expiration, issuer, audience, blacklistService);
});

// Configurações de infraestrutura e aplicação
builder.Services.ConfigurePersistenceApp();
builder.Services.ConfigureApplicationApp();
builder.Services.AddPersistenceServices();

builder.Services.AddControllers();

// Swagger com segurança
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManaFood API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo usando o prefixo 'Bearer'. Exemplo: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddOpenApi();

// Injeção de dependências
builder.Services.AddTransient<UserValidationService>();
builder.Services.AddHttpClient<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<IPaymentStatusService, MercadoPagoStatusService>();
builder.Services.AddScoped<IPaymentProviderConfig, PaymentProviderConfig>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();

// Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

var app = builder.Build();

// Aplica migrations automaticamente
await app.MigrateDbContextAsync();

// Middlewares
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManaFood API V2");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ManaFood.WebAPI.Middlewares.JwtAuthenticationMiddleware>();

app.MapControllers();
app.Run();
