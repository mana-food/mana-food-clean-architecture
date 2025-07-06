namespace ManaFood.WebAPI.Webhooks.MercadoPago;

public class MercadoPagoWebhookPayload
{
    public string Type { get; set; } = string.Empty;
    public MercadoPagoData Data { get; set; } = new();
}

public class MercadoPagoData
{
    public string Id { get; set; } = string.Empty;
}
