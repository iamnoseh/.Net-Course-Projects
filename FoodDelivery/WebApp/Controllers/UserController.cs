using Domain.DTOs.OrderDto;
using Domain.DTOs.UserDtos;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserServices services):Controller
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser(CreateUserDto dto)
    {
        var res = await services.CreateUser(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
    {
        var res = await services.UpdateUser(dto);
        return Ok(res);
    }
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var res = await services.DeleteUser(id);
        return Ok(res);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var res = await services.GetUsers();
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(int id)
    {
        var res = await services.GetUserById(id);
        return Ok(res);
    }
}