using Domain.DTOs.Student;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService service) :  ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var res =  await service.GetAllStudents();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var res = await service.GetStudent(id);
        return Ok(res);
    }
    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
    {
        var res = await service.CreateStudent(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(UpdateStudentDto dto)
    {
        var res = await service.UpdateStudent(dto);
        return Ok(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var res = await service.DeleteStudent(id);
        return Ok(res);
    }
}