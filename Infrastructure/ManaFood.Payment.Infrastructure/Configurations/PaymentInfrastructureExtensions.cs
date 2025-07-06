using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ManaFood.Payment.Domain.Interfaces;
using ManaFood.Payment.Infrastructure.Services;

namespace ManaFood.Payment.Infrastructure.Configurations
{
    public static class PaymentInfrastructureExtensions
    {
        public static IServiceCollection AddPaymentInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Registra o serviço de pagamento usando HttpClient
            services.AddHttpClient<IPaymentService, PaymentService>();

            return services;
        }
    }
}
