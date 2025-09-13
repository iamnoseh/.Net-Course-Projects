using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Restaurant:BaseEntity
{
    [Required]
    public required string Name { get; set; }
    public required string Address { get; set; }
    public decimal Rating { get; set; }
    public required string WorkingHours { get; set; }
    public string? Description { get; set; }
    [Phone]
    public required string ContactPhone { get; set; }
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryPrice { get; set; }
    public Menu Menu { get; set; }
}