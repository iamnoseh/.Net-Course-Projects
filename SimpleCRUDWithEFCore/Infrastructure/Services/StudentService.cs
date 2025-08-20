using System.Net;
using Domain.DTOs.Student;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context) : IStudentService
{
    public async Task<Response<string>> CreateStudent(CreateStudentDto dto)
    {
        try
        {
            var newStudent = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Age = dto.Age,
                Gender = dto.Gender,
                CourseId = dto.CourseId
            };
            await context.Students.AddAsync(newStudent);
            var res =  await context.SaveChangesAsync();
            return res >0 
                ? new Response<string>(HttpStatusCode.Created,"Student created successfully")
                : new Response<string>(HttpStatusCode.BadRequest,"Failed to create student");
        }
        catch (Exception e)
        {
            throw new Exception("Error creating student");
        }
    }

    public async Task<Response<string>> UpdateStudent(UpdateStudentDto dto)
    {
        var oldStudent = await context.Students.FirstOrDefaultAsync(x=> x.Id == dto.Id);
        if(oldStudent == null) { return new Response<string>(HttpStatusCode.NotFound,"Student not found");}
        oldStudent.FirstName = dto.FirstName;
        oldStudent.LastName = dto.LastName;
        oldStudent.Email = dto.Email;
        oldStudent.Age = dto.Age;
        oldStudent.Gender = dto.Gender;
        context.Students.Update(oldStudent);
        var res = await context.SaveChangesAsync();
        return res > 0 
            ?  new Response<string>(HttpStatusCode.OK,"Student updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Failed to create student");
    }

    public async Task<Response<string>> DeleteStudent(int id)
    {
        var  oldStudent = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
        if(oldStudent == null) { return new Response<string>(HttpStatusCode.NotFound,"Student not found"); }
        context.Students.Remove(oldStudent);
        var res = await context.SaveChangesAsync();
        return res > 0
            ?   new Response<string>(HttpStatusCode.OK,"Student deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Failed to deleting student");
    }

    public async Task<Response<GetStudentDto>> GetStudent(int id)
    {
        var student = await context.Students.FirstOrDefaultAsync(x=> x.Id == id);
        if(student == null) { return new Response<GetStudentDto>(HttpStatusCode.NotFound,"Student not found"); }

        var res = new GetStudentDto()
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Age = student.Age,
            Gender = student.Gender,
        };
        return new Response<GetStudentDto>(res);
    }

    public async Task<Response<List<GetStudentDto>>> GetAllStudents()
    {
        var students = await context.Students.ToListAsync();
        if(students.Count == 0) { return new Response<List<GetStudentDto>>(HttpStatusCode.NotFound,"No students found"); }

        var res = students.Select(x => new GetStudentDto()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Age = x.Age,
            Gender = x.Gender,
        }).ToList();
        return new Response<List<GetStudentDto>>(res);
    }
}