namespace Domain.DTOs.OrderDetailDto;

public class GetOrderDetailDto:UpdateOrderDetailDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}