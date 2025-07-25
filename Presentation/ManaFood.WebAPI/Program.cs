using ManaFood.Infrastructure.Configurations;
using ManaFood.Application.Configurations;
using ManaFood.Infrastructure.Services;
using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Services.MercadoPago;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp();
builder.Services.AddPersistenceServices();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddTransient<UserValidationService>();
builder.Services.AddHttpClient<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<IPaymentStatusService, MercadoPagoStatusService>();
builder.Services.AddScoped<IPaymentProviderConfig, PaymentProviderConfig>();
builder.Services.AddScoped<IPaymentProviderConfig, PaymentProviderConfig>();

var app = builder.Build();

// Aplica migrations automaticamente
await app.MigrateDbContextAsync();

// Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManaFood API V2");
    c.RoutePrefix = string.Empty;
});

// Pipeline HTTP
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();