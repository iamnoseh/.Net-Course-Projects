using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.Student;

public class CreateStudentDto
{
    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "FirstName must be between 3 and 20 characters")]
    public required string FirstName { get; set; } 
    [StringLength(20,MinimumLength = 3, ErrorMessage = "LastName must be between 3 and 20 characters")]
    public string? LastName { get; set; }
    [Range(12,45,ErrorMessage = "Year must be between 12 and 45")]
    public int Age { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public int CourseId { get; set; }
}