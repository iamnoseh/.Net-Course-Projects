using Domain.DTOs.ProductDto;

namespace Domain.DTOs.OrderDto;

public class GetOrderDto:UpdateOrdesDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}