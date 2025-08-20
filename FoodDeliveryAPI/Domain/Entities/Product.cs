using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product:BaseEntity
{
    [Required]
    [StringLength(50,MinimumLength =2)]
    public required string  Name {get;set;}
    public string?  Description {get;set;}
    public decimal  Price {get;set;}
    public bool IsAvailable {get;set;}
    
    //navigation
    public int CategoryId {get;set;}
    public Category Category {get;set;}
    
    public OrderItem  OrderItem {get;set;}
}