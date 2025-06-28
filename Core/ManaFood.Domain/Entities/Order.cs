namespace ManaFood.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime? OrderConfirmationTime { get; init; }
    public OrderStatus OrderStatus { get; set; }
    public double TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<OrderProduct> Products { get; set; } = new();

    public void CalculateTotal()
    {
        TotalAmount = Products.Sum(p => p.Product.UnitPrice * p.Quantity);
    }
}
public enum OrderStatus
{
    AGUADANDO_PAGAMENTO = 0,
    CANCELADO = 1,
    RECEIVED = 2,
    PREPARING = 3,
    READY = 4,
    FINALIZED = 5
}

public enum PaymentMethod
{
    PIX = 0,
    CREDIT_CARD = 1,
    DEBIT_CARD = 2
}