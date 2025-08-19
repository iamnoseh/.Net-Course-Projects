using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Author;

public class CreateAuthorDto
{
    [Required]
    [StringLength(50, MinimumLength = 3,ErrorMessage = "Harfho boyad az 3 to 50 simvol doshta boshand!")]
    public required string FullName { get; set; }
    public int BirthYear { get; set; }
    public string? Country { get; set; }
}