using Domain.DTOs.AuthDto;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Responce;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Infrastructure.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtService _jwtService;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        JwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<Responce<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new Responce<AuthResponseDto>(HttpStatusCode.Unauthorized, "Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return new Responce<AuthResponseDto>(HttpStatusCode.Unauthorized, "Invalid email or password");
            }

            var token = await _jwtService.GenerateToken(user);
            var userInfo = _jwtService.MapToUserInfo(user);

            var response = new AuthResponseDto
            {
                Token = token,
                RefreshToken = "", // You can implement refresh token logic here
                Expiration = DateTime.UtcNow.AddMinutes(60),
                User = userInfo
            };

            return new Responce<AuthResponseDto>(response);
        }
        catch (Exception ex)
        {
            return new Responce<AuthResponseDto>(HttpStatusCode.InternalServerError, "An error occurred during login");
        }
    }

    public async Task<Responce<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new Responce<AuthResponseDto>(HttpStatusCode.BadRequest, "User with this email already exists");
            }

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Name = registerDto.Name,
                PhoneNumber = registerDto.Phone,
                Address = registerDto.Address,
                RegistrationDate = DateTime.UtcNow,
                Role = registerDto.Role,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new Responce<AuthResponseDto>(HttpStatusCode.BadRequest, errors);
            }

            // Add user to role
            var roleName = registerDto.Role.ToString();
            await _userManager.AddToRoleAsync(user, roleName);

            var token = await _jwtService.GenerateToken(user);
            var userInfo = _jwtService.MapToUserInfo(user);

            var response = new AuthResponseDto
            {
                Token = token,
                RefreshToken = "",
                Expiration = DateTime.UtcNow.AddMinutes(60),
                User = userInfo
            };

            return new Responce<AuthResponseDto>(response);
        }
        catch (Exception ex)
        {
            return new Responce<AuthResponseDto>(HttpStatusCode.InternalServerError, "An error occurred during registration");
        }
    }
}


