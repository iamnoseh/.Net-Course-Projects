namespace Domain.DTOs.OrderItems;

public class GetOrderItemDto:UpdateOrderItemDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}