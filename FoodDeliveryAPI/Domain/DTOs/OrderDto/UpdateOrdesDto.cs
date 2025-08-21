using Domain.Enums;

namespace Domain.DTOs.OrderDto;

public class UpdateOrdesDto
{
    public int Id{get;set;}
    public DateTime OrderDate{get;set;}
    public decimal TotalAmount{get;set;}
    public Status Status{get;set;}
    public int CourierId{get;set;}
}