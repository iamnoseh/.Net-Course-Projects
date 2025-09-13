namespace Domain.DTOs.RestourantDtos;

public class GetRestourantDto:UpdateRestourantDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}