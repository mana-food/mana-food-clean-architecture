using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ManaFood.Application.Interfaces;
using ManaFood.Domain.Entities;
using QRCoder;
using ManaFood.Application.Dtos;

namespace ManaFood.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentProviderConfig _config;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(HttpClient httpClient, IPaymentProviderConfig config, IOrderRepository orderRepository)
        {
            _httpClient = httpClient;
            _config = config;
            _orderRepository = orderRepository;
        }

        public async Task<CreatePaymentResponse> CreatePaymentAsync(Guid orderId)
        {
            Console.WriteLine($"🔍 Buscando pedido {orderId} no banco...");
            var order = await _orderRepository.GetByIdWithProductsAsync(orderId);

            if (order == null)
                throw new Exception($"Pedido {orderId} não encontrado.");

            Console.WriteLine($"📦 Pedido encontrado! Total: R${order.TotalAmount:F2}");

            var externalReference = order.Id.ToString();

            var items = order.Products.Select(p => new
            {
                sku_number = p.Product.Id.ToString(),
                category = "marketplace",
                title = p.Product.Name,
                description = p.Product.Description ?? "Produto sem descrição",
                unit_price = p.Product.UnitPrice,
                quantity = p.Quantity,
                unit_measure = "unit",
                total_amount = p.Product.UnitPrice * p.Quantity
            }).ToArray();

            var body = new
            {
                external_reference = externalReference,
                title = $"Pedido {externalReference[..8]}", // primeiros 8 chars do GUID
                description = $"Pedido com {items.Length} item(ns)",
                notification_url = _config.NotificationUrl,
                total_amount = order.TotalAmount,
                items = items,
                cash_out = new { amount = 0 }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"https://api.mercadopago.com/instore/orders/qr/seller/collectors/{_config.UserId}/pos/{_config.ExternalPosId}/qrs"
            );

            var idempotencyKey = Guid.NewGuid().ToString();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.AccessToken);
            request.Headers.Add("X-Idempotency-Key", idempotencyKey);

            var json = JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine("📨 Enviando payload para Mercado Pago...");
            Console.WriteLine($"🔗 Endpoint: {request.RequestUri}");
            Console.WriteLine($"🧾 External Reference: {externalReference}");
            Console.WriteLine($"📦 JSON:\n{json}");

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("❌ Erro ao criar pagamento:");
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("✅ Pagamento criado com sucesso!");
            Console.WriteLine("📥 Resposta:\n" + responseContent);

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;

            var qrData = root.GetProperty("qr_data").GetString();
            var paymentId = root.GetProperty("id").GetString();

            string qrCodeBase64 = GenerateQrCodeBase64(qrData!);

            return new CreatePaymentResponse
            {
                PaymentId = paymentId!,
                QrData = qrData!,
                QrCodeBase64 = qrCodeBase64
            };
        }

        private string GenerateQrCodeBase64(string qrData)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = qrCode.GetGraphic(20);

            return Convert.ToBase64String(qrBytes);
        }
    }
}
