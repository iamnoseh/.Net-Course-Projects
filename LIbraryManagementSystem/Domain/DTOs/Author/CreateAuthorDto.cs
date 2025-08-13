namespace Domain.DTOs.Author;

public class CreateAuthorDto
{
    public string FullName { get; set; }
    public int BirthYear { get; set; }
    public string Country { get; set; }
}