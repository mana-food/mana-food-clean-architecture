namespace ManaFood.Payment.Domain.Entities
{
    public class Payment
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string QrCodeUrl { get; set; }
    }
}