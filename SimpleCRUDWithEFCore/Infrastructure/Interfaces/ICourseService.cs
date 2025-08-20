using Domain.DTOs.Course;
using Domain.Entities;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Response<string>> CreateCourse(CreateCourseDto request);
    Task<Response<string>> UpdateCourse(UpdateCourseDto request);
    Task<Response<string>>  DeleteCourse(int id);
    Task<Response<List<GetCourseDto>>> GetAllCourses();
    Task<Response<GetCourseDto>> GetCourseById(int id);
    Task<Response<List<GetCourseStudentsDto>>>  GetAllCourseStudents();
    Task<Response<GetCourseStudentsDto>>  GetCourseStudents(int courseId);
}