namespace ManaFood.Application.Dtos;

public record ItemDto : BaseDto
{
    public string Name { get; init; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}
