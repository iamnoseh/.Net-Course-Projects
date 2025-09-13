using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }
    [ForeignKey("MenuItemId")]
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? SpecialInstructions { get; set; }
    public Order? Order { get; set; }
    public Menu? Menu { get; set; }
}