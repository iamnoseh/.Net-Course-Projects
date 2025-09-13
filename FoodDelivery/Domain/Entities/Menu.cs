using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Menu : BaseEntity
{
    [Required] 
    public int RestaurantId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string Category { get; set; }
    public bool IsAvailable { get; set; }
    public int PreparationTime { get; set; }
    public int Weight { get; set; }
    public string? PhotoUrl { get; set; }
    
    List<OrderDetail>? OrderDetails { get; set; }
    public Restaurant? Restourant { get; set; }
}