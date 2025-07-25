using ManaFood.Application.Interfaces;
using Microsoft.Extensions.Configuration;

public class PaymentProviderConfig : IPaymentProviderConfig
{
    private readonly IConfiguration _configuration;

    public PaymentProviderConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string AccessToken => _configuration["MercadoPago:AccessToken"];
    public string NotificationUrl => _configuration["MercadoPago:NotificationUrl"];
}