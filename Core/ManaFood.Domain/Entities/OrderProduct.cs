namespace ManaFood.Domain.Entities;

public class OrderProduct : BaseEntity
{
    public Guid OrderId { get; init; }
    public Order? Order { get; set; }
    public Guid ProductId { get; init; }
    public required Product Product { get; set; }
    public double Quantity { get; set; }
    public double Subtotal => Product.UnitPrice * Quantity;
}