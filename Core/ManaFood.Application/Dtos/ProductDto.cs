namespace ManaFood.Application.Dtos;

public record ProductDto : BaseDto
{
    public string Name { get; init; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public double UnitPrice { get; set; }
    public List<Guid> ItemIds { get; set; } = new();
}
