using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("login")]
    [Authorize("Mentor,Admin")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var res = await authService.Login(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var res = await authService.Register(dto);
        return StatusCode(res.StatusCode, res);
    }
}


