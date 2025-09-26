namespace Domain.Dtos.Book;

public class GetBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}