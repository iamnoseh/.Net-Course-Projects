using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class CreateProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public IFormFile? Image { get; set; }
}