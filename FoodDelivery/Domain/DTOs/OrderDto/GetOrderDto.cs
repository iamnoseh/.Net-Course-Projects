namespace Domain.DTOs.OrderDto;

public class GetOrderDto:UpdateOrderDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}