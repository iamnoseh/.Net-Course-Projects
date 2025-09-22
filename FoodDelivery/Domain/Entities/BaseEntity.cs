namespace Domain.Entities;

public class BaseEntity
{
    public int Id{get;set;}
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate{get;set;} =  DateTime.UtcNow;
}