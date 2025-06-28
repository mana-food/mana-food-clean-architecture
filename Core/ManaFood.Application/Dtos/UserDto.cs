namespace ManaFood.Application.Dtos;

public record UserDto : BaseDto
{
    public string Email { get; set; }
    public string Name { get; init; }
    public string Cpf { get; set; }
    public DateOnly Birthday { get; set; }
    public int UserType { get; set; } = default;
}