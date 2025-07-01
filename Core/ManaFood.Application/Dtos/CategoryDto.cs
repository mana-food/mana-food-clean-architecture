namespace ManaFood.Application.Dtos;

public record CategoryDto : BaseDto
{
    public required string Name { get; init; }
}
