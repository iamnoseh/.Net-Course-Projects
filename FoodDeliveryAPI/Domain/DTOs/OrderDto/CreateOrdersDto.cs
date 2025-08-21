using Domain.Enums;

namespace Domain.DTOs.OrderDto;

public class CreateOrdersDto
{
    public Status Status {get;set;}
    public int CourierId {get;set;}  
}