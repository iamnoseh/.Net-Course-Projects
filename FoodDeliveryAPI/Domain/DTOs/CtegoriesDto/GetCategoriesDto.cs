using Domain.DTOs.ProductDto;

namespace Domain.DTOs.CtegoriesDto;

public class GetCategoriesDto:UpdateProductDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}