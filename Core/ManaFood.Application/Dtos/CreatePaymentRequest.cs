namespace ManaFood.Application.Dtos
{
    public class CreatePaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PayerEmail { get; set; } = string.Empty;
        public string PayerFirstName { get; set; } = string.Empty;
        public string PayerLastName { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
    }
}