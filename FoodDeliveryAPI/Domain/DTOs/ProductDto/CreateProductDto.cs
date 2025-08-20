using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.ProductDto;

public class CreateProductDto
{
    [Required]
    public required string  Name{get;set;}
    public string?  Description{get;set;}
    public decimal  Price{get;set;}
    public int    CategoryId{get;set;}
    public bool IsAvailable{get;set;}
}