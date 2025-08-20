namespace Domain.DTOs.ProductDto;

public class GetProductDto:UpdateProductDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}