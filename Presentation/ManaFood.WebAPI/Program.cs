using ManaFood.Infrastructure.Configurations;
using ManaFood.Application.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ManaFood.Application.Interfaces.Services;
using ManaFood.Application.Services;
using ManaFood.Infrastructure.Auth;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Registrar JwtService para uso em controllers/filtros
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

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManaFood API", Version = "v1" });

    // Adiciona a configuração de segurança JWT
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

builder.Services.AddTransient<UserValidationService>();

builder.Services.AddScoped<IAuthAppService, AuthAppService>();

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

app.UseMiddleware<ManaFood.WebAPI.Middlewares.JwtAuthenticationMiddleware>();
app.UseRouting();
app.UseMiddleware<ManaFood.WebAPI.Middlewares.JwtAuthenticationMiddleware>();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManaFood API V2");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

// Ordem correta dos middlewares
app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
