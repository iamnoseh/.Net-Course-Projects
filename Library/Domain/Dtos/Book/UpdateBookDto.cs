namespace Domain.Dtos.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsPublic { get; set; }
}