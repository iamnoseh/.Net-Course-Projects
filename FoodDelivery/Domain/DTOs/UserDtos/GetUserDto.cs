namespace Domain.DTOs.UserDtos;

public class GetUserDto:UpdateUserDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}