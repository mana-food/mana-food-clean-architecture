namespace ManaFood.Application.Dtos;

public record ProductOrderDto
{
    public Guid ProductId { get; init; }
    public required double Quantity { get; init; }
}