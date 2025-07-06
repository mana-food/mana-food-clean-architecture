using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ManaFood.Payment.Domain.Entities;
using ManaFood.Payment.Domain.Interfaces;

namespace ManaFood.Payment.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PaymentService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GenerateQrCodeAsync(ManaFood.Payment.Domain.Entities.Payment payment)
    {
        // Pega os dados do appsettings.json
        var accessToken = _configuration["MercadoPago:AccessToken"];
        var notificationUrl = _configuration["MercadoPago:NotificationUrl"];

        if (string.IsNullOrWhiteSpace(accessToken))
            throw new InvalidOperationException("MercadoPago access token is missing.");

        var request = new
        {
            items = new[]
            {
                new
                {
                    title = $"Pedido #{payment.OrderId}",
                    quantity = 1,
                    currency_id = "BRL",
                    unit_price = payment.Amount
                }
            },
            notification_url = notificationUrl
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.PostAsync("https://api.mercadopago.com/checkout/preferences", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao gerar QR Code: {response.StatusCode} - {error}");
        }

        var resultJson = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MercadoPagoResponse>(resultJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.InitPoint ?? throw new Exception("Resposta do Mercado Pago inválida.");
    }
}

internal class MercadoPagoResponse
{
    public string Id { get; set; } = default!;
    public string InitPoint { get; set; } = default!;
}
