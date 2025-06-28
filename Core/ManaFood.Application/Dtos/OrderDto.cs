namespace ManaFood.Application.Dtos;

public record OrderDto : BaseDto
{
    public int OrderStatus { get; set; }
    public double TotalAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<ProductOrderDto> Products { get; set; } = new();
}