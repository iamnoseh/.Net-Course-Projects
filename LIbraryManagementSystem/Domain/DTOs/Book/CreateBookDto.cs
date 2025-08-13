namespace Domain.DTOs;

public class CreateBookDto
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string Genre { get; set; }
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

