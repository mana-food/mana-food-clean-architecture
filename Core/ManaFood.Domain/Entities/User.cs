namespace ManaFood.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public DateOnly Birthday { get; set; }
    public UserType UserType { get; set; }
}

public enum UserType
{
    ADMIN = 0,
    CUSTOMER = 1,
    KITCHEN = 2,
    OPERATOR = 3,
    MANAGER = 4
}