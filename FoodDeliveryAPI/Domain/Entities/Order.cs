using Domain.Enums;

namespace Domain.Entities;

public class Order:BaseEntity
{
    
    public DateTime OrderDate {get;set;}
    public decimal TotalAmount {get;set;}
    public Status Status {get;set;}
    //navigation
    public int CourierId {get;set;}
    public Courier Courier {get;set;}

    public List<OrderItem> OrderItems { get; set; } = [];
}