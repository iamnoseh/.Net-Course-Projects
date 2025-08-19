namespace Domain.Entities;

public class Author : BaseEntity
{
    public required string FullName { get; set; }
    public int BirthYear { get; set; }
    public string? Country { get; set; }
}