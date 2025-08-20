
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Courier : BaseEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;
    public string? LastName{get;set;}
    [Phone]
    public string? Phone {get;set;}
    public bool IsAvailable{get;set;}
    
    //navigation
    public List<Order> Orders {get;set;}
}