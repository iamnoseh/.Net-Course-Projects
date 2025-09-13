using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    public User? User { get; set; }
    public Courier? Courier { get; set; }
    public List<OrderDetail>? OrderDetails { get; set; }
}