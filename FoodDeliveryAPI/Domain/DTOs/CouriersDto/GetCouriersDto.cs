namespace Domain.DTOs.CouriersDto;

public class GetCouriersDto:UpdateCouriersDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}