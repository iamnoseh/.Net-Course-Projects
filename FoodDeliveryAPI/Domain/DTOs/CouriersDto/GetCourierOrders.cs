using Domain.DTOs.OrderDto;
using Domain.DTOs.OrderItems;

namespace Domain.DTOs.CouriersDto;

public class GetCourierOrders
{
    public int CourierId {get;set;}
    public string CourierName {get;set;}
    public decimal TotalPrice {get;set;}
    public List<GetOrder>  Orders {get;set;}
}

public class GetOrder
{
    public int OrderId {get;set;}
    public decimal TotalPrice { get; set; }
    public List<GetOrderItemDto> Items {get;set;}
}