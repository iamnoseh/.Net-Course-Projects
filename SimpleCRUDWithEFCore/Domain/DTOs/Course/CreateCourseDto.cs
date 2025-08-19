using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Course;

public class CreateCourseDto
{
    [Required]
    [StringLength(50, MinimumLength = 3,ErrorMessage ="Name must be between 3 and 50 characters")]
    public required string Name { get; set; }
    public string? Description { get; set; }
}