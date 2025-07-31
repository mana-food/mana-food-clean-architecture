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

        public async Task<(string status, string orderId)> GetPaymentStatusAsync(string paymentId)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config.AccessToken);

            if (!paymentId.StartsWith("ORD"))
                throw new ArgumentException("O paymentId fornecido não é um ID válido de Order (esperado prefixo 'ORD').");

            var response = await _httpClient.GetAsync($"https://api.mercadopago.com/v1/orders/{paymentId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            var payments = doc.RootElement.GetProperty("transactions").GetProperty("payments");

            if (payments.GetArrayLength() == 0)
                throw new Exception("Nenhum pagamento associado à Order.");

            var firstPayment = payments[0];
            var status = firstPayment.GetProperty("status").GetString()!;
            var orderId = doc.RootElement.GetProperty("external_reference").GetString()!;

            return (status, orderId);

        }

    }
}