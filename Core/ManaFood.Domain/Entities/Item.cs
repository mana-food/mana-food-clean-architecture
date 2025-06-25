namespace ManaFood.Domain.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
