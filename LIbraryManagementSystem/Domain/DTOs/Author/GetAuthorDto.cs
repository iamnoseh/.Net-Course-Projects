namespace Domain.DTOs.Author;

public class GetAuthorDto : UpdateAuthorDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}