using ManaFood.Infrastructure.Configurations;
using ManaFood.Application.Configurations;
using ManaFood.Payment.Infrastructure.Configurations;
using ManaFood.WebAPI.Mock;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddPaymentInfrastructure(builder.Configuration);
builder.Services.AddHttpClient<MockPaymentSender>();

builder.Services.AddTransient<UserValidationService>();

var app = builder.Build();

// Aplica migrations automaticamente
await app.MigrateDbContextAsync();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManaFood API V2");
    c.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
