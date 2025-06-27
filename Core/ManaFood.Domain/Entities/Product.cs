namespace ManaFood.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double UnitPrice { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Item> Items { get; set; } = new();
}
