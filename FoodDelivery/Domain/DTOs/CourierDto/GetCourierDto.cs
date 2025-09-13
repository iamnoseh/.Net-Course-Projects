namespace Domain.DTOs.CourierDto;

public class GetCourierDto:UpdateCourierDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}