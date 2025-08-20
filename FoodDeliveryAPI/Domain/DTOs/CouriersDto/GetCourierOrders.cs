using Domain.DTOs.OrderDto;

namespace Domain.DTOs.CouriersDto;

public class GetCourierOrders
{
    public int CourierId {get;set;}
    public string CourierName {get;set;}
    public decimal TotalPrice {get;set;}
    public List<GetOrderDto>  Orders {get;set;}
}