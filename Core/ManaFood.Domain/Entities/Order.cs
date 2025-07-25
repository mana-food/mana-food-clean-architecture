using ManaFood.Domain.Enums;

namespace ManaFood.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime? OrderConfirmationTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public double TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<OrderProduct> Products { get; set; } = new();
    public DateTime UpdatedAt { get; set; }
    public void CalculateTotal()
    {
        TotalAmount = Products.Sum(p => p.Product.UnitPrice * p.Quantity);
    }
    public void SetStatus(OrderStatus status)
    {
        OrderStatus = status;
        UpdatedAt = DateTime.UtcNow;

        if (status == OrderStatus.APROVADO)
            OrderConfirmationTime = DateTime.UtcNow;
    }
}