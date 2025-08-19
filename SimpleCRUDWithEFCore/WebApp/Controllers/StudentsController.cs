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

    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
    {
        var res = await service.CreateStudent(dto);
        return Ok(res);
    }
}