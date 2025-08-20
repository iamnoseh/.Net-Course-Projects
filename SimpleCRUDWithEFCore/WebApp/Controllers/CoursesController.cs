using Domain.DTOs.Course;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService service): Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
    {
        var res = await service.CreateCourse(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
    {
        var res = await service.UpdateCourse(dto);
        return Ok(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var res = await service.DeleteCourse(id);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var res = await service.GetAllCourses();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var res = await service.GetCourseById(id);
        return Ok(res);
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetCoursesStudents()
    {
        var res = await service.GetAllCourseStudents();
        return Ok(res);
    }

    [HttpGet("{id}-students")]
    public async Task<IActionResult> GetCoursesStudents(int id)
    {
        var res =  await service.GetCourseStudents(id);
        return Ok(res);
    }
}