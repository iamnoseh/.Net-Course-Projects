using Domain.Models;

namespace Infrastructure.Interefaces;

public interface ITeacherService
{
    bool CreateTeacher(Teacher teacher);
    bool UpdateTeacher(Teacher teacher);
    bool DeleteTeacher(int id);
    Teacher GetTeacherById(int id);
    List<Teacher> GetAllTeachers();
}