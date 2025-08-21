using Domain.Enums;

namespace Domain.DTOs.OrderDto;

public class CreateOrdersDto
{
    public decimal TotalAmount {get;set;}
    public Status Status {get;set;}
    public int CourierId {get;set;}  
}