using ManaFood.Infrastructure.Configurations;
using ManaFood.Application.Configurations;
using ManaFood.Infrastructure.Services;
using ManaFood.Infrastructure.Services.MercadoPago;

using ManaFood.Application.Interfaces;
using ManaFood.Application.Interfaces.Services;
using ManaFood.Application.Services;

using Microsoft.OpenApi.Models;
using ManaFood.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAuth(builder.Configuration);

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

app.UseMiddleware<JwtAuthenticationMiddleware>();

app.MapControllers();

app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));

app.Run();
