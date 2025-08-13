namespace Domain.DTOs;

public class GetBookDto : UpdateBookDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}