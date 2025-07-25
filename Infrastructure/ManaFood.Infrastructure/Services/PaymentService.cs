using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ManaFood.Application.Interfaces;

namespace ManaFood.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentProviderConfig _config;

        public PaymentService(HttpClient httpClient, IPaymentProviderConfig config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> CreatePaymentAsync(Guid orderId, decimal amount)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.mercadopago.com/v1/orders"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.AccessToken);

            var body = new
            {
                type = "online",
                external_reference = orderId.ToString(),
                notification_url = $"{_config.NotificationUrl}?source_news=webhooks",
                total_amount = amount,
                payer = new
                {
                    email = "cliente@manafood.com.br",
                    entity_type = "individual",
                    first_name = "Cliente",
                    last_name = "ManaFood",
                    identification = new
                    {
                        type = "CPF",
                        number = "87245786062"
                    }
                },
                transactions = new
                {
                    payments = new[]
                    {
                        new
                        {
                            amount = amount,
                            payment_method = new
                            {
                                id = "pix",
                                type = "bank_transfer"
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}