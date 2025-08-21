using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category:BaseEntity
{
    [Required]
    [StringLength(50,MinimumLength = 2)]
    public string  Name{get;set;}
    
    //navigation
    public List<Product> Products{get;set;}
}