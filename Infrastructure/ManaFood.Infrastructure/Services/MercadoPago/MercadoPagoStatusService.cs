using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ManaFood.Application.Interfaces;

namespace ManaFood.Infrastructure.Services.MercadoPago
{
    public class MercadoPagoStatusService : IPaymentStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentProviderConfig _config;

        public MercadoPagoStatusService(HttpClient httpClient, IPaymentProviderConfig config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<(string status, string orderId)> GetPaymentStatusAsync(string merchantOrderId)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config.AccessToken);

            var response = await _httpClient.GetAsync($"https://api.mercadopago.com/merchant_orders/{merchantOrderId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            var root = doc.RootElement;
            var status = root.GetProperty("status").GetString()!; // "closed" [pedido foi pago com sucesso], "opened" [pedido ainda não foi pago]
            var orderId = root.GetProperty("external_reference").GetString()!;

            return (status, orderId);
        }
    }
}