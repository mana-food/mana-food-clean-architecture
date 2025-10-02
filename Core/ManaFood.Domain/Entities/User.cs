using ManaFood.Domain.Enums;

namespace ManaFood.Domain.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    public required string Password { get; set; }
    public DateOnly Birthday { get; set; }
    public UserType UserType { get; set; }
}