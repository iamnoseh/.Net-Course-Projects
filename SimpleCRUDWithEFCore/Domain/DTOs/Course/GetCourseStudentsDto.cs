using Domain.DTOs.Student;

namespace Domain.DTOs.Course;

public class GetCourseStudentsDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public List<GetStudentDto> Students { get; set; }
}