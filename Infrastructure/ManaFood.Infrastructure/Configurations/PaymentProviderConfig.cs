using ManaFood.Application.Interfaces;
using Microsoft.Extensions.Configuration;

public class PaymentProviderConfig : IPaymentProviderConfig
{
    public string AccessToken => Environment.GetEnvironmentVariable("MERCADOPAGO_ACCESS_TOKEN")!;
    public string NotificationUrl => Environment.GetEnvironmentVariable("MERCADOPAGO_NOTIFICATION_URL")!;
    public long UserId => long.Parse(Environment.GetEnvironmentVariable("MERCADOPAGO_USER_ID")!);
    public string StoreId => Environment.GetEnvironmentVariable("MERCADOPAGO_STORE_ID")!;
    public string ExternalStoreId => Environment.GetEnvironmentVariable("MERCADOPAGO_EXTERNAL_STORE_ID")!;
    public string ExternalPosId => Environment.GetEnvironmentVariable("MERCADOPAGO_EXTERNAL_POS_ID")!;
}