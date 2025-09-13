namespace Domain.DTOs.MenuDtos;

public class GetMenuDto:UpdateMenuDto
{
    public DateTime CreateDate{get;set;}
    public DateTime UpdateDate{get;set;}
}