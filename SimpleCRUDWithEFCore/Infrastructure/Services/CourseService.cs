using System.Net;
using Domain.DTOs.Course;
using Domain.DTOs.Student;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context) : ICourseService
{
    public async Task<Response<string>> CreateCourse(CreateCourseDto request)
    {
        var newCourse = new Course()
        {
            Name = request.Name,
            Description = request.Description,
        };
        await context.Courses.AddAsync(newCourse);
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Course created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Course could not be created");
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto request)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (course == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        course.Name = request.Name;
        course.Description = request.Description;
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Course updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Course could not be updated");
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        context.Courses.Remove(course);
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Course deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Course could not be deleted");
    }

    public async Task<Response<List<GetCourseDto>>> GetAllCourses()
    {
        var courses = await context.Courses.ToListAsync();
        if (courses.Count == 0) return new Response<List<GetCourseDto>>(HttpStatusCode.NotFound, "Courses not found");
        var res = courses.Select(c => new GetCourseDto()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToList();
        return new Response<List<GetCourseDto>>(res);
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course == null) return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found");
        var res = new GetCourseDto()
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        };
        return new Response<GetCourseDto>(res);
    }

    public async Task<Response<List<GetCourseStudentsDto>>> GetAllCourseStudents()
    {
        var res = await context.Courses.Include(x => x.Students)
            .Select(c => new GetCourseStudentsDto()
            {
                CourseId = c.Id,
                CourseName = c.Name,
                Students = c.Students.Select(s => new GetStudentDto()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age,
                    Email = s.Email,
                    Gender = s.Gender
                }).ToList()
            }).ToListAsync();
        return res.Count > 0
            ? new Response<List<GetCourseStudentsDto>>(res)
            : new Response<List<GetCourseStudentsDto>>(HttpStatusCode.NotFound, "Courses not found");
    }

    public async Task<Response<GetCourseStudentsDto>> GetCourseStudents(int courseId)
    {
        var res = await context.Courses.Include(x => x.Students)
            .Where(c => c.Id == courseId)
            .Select(e => new GetCourseStudentsDto()
            {
                CourseId = e.Id,
                CourseName = e.Name,
                Students = e.Students.Select(s => new GetStudentDto()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age,
                    Email = s.Email,
                    Gender = s.Gender
                }).ToList()
            }).FirstOrDefaultAsync();
        return res == null
            ? new Response<GetCourseStudentsDto>(HttpStatusCode.NotFound, "Course not found")
            : new Response<GetCourseStudentsDto>(res);
    }
}