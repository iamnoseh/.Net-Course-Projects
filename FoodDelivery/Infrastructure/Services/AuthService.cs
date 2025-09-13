using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthService(DataContext context, IConfiguration configuration) : IAuthService
{
    public async Task<Responce<string>> Login(LoginDto loginDto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return new Responce<string>(HttpStatusCode.BadRequest, "Your email or password is incorrect");
            }

            var valid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!valid)
            {
                return new Responce<string>(HttpStatusCode.Unauthorized, "Your email or password is incorrect");
            }

            var token = GenerateJwtToken(user.Id, user.Name, user.Email, user.Role.ToString());
            return new Responce<string>(token);
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> Register(CreateUserDto registerDto)
    {
        try
        {
            var exists = await context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (exists)
            {
                return new Responce<string>(HttpStatusCode.Conflict, "Email already registered");
            }

            var userService = new UserServices(context);
            var createRes = await userService.CreateUser(registerDto);
            if (createRes.StatusCode != (int)HttpStatusCode.Created)
            {
                return new Responce<string>((HttpStatusCode)createRes.StatusCode, createRes.Message);
            }
            return new Responce<string>(HttpStatusCode.OK,"User registration successful");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private string GenerateJwtToken(int userId, string name, string email, string role)
    {
        var jwtSection = configuration.GetSection("JWT");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var secret = jwtSection["Key"];
        var expiresMinutes = int.TryParse(jwtSection["ExpiresMinutes"], out var m) ? m : 120;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, userId.ToString()),
            new(JwtRegisteredClaimNames.Name, name),
            new(JwtRegisteredClaimNames.Email, email),
            new("role", role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
}


