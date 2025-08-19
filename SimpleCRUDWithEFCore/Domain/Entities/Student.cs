using System.ComponentModel.DataAnnotations;
using Domain.Enums;
namespace Domain.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(20)]
    public string? LastName { get; set; }
    [Range(12,45)]
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    
    //navigation properties
    public int CourseId { get; set; }
    public Course? Course { get; set; }
}