namespace ManaFood.Application.Dtos;

public record ProductOrderDto
{
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }
}