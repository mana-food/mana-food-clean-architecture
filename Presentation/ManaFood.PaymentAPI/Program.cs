var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ManaFood.Payment.Domain.Interfaces.IPaymentService, ManaFood.Payment.Infrastructure.Services.PaymentService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ManaFood.Payment.Application.UseCases.CreatePayment.CreatePaymentHandler).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
