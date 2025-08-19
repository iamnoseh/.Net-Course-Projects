using Domain.Enums;

namespace Domain.DTOs.Student;

public class UpdateStudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; } 
    public Gender Gender { get; set; }
}