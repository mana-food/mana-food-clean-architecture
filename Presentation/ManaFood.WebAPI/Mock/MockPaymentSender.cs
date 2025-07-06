using System.Net.Http.Json;

namespace ManaFood.WebAPI.Mock
{
    public class MockPaymentSender
    {
        private readonly HttpClient _httpClient;

        public MockPaymentSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMockPayment(Guid orderId)
        {
            var payload = new { OrderId = orderId };
            await _httpClient.PostAsJsonAsync("/webhooks/mercadopago", payload);
        }
    }
}