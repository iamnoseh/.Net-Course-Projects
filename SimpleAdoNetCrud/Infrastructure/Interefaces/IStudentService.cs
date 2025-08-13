using Domain.Models;

namespace Infrastructure.Interefaces;

public interface IStudentService
{
    bool CreateStudent(Student student);
    bool UpdateStudent(Student student);
    bool DeleteStudent(int id);
    Student GetStudentById(int id);
    List<Student> GetAllStudents();
}