using Domain.DTOs.Student;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<string>> CreateStudent(CreateStudentDto dto);
    Task<Response<string>> UpdateStudent(UpdateStudentDto dto);
    Task<Response<string>> DeleteStudent(int id);
    Task<Response<GetStudentDto>> GetStudent(int id);
    Task<Response<List<GetStudentDto>>> GetAllStudents();
}