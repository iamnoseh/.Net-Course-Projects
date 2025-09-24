namespace Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImagePath { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}