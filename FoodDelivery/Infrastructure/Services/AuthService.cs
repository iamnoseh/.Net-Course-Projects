using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : IAuthService
{
    public async Task<Responce<string>> Login(LoginDto loginDto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new Responce<string>(HttpStatusCode.BadRequest, "Your email or password is incorrect");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return new Responce<string>(HttpStatusCode.Unauthorized, "Your email or password is incorrect");
            }

            var roles = await userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user.Id, user.Name, user.Email!, roles,user.Phone);
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
            var exists = await userManager.FindByEmailAsync(registerDto.Email);
            if (exists != null)
            {
                return new Responce<string>(HttpStatusCode.Conflict, "Phone already registered");
            }
            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Phone = registerDto.Phone,
                Address = registerDto.Address,
                RegistrationDate = registerDto.RegistrationDate == default ? DateTime.UtcNow : registerDto.RegistrationDate,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            var createRes = await userManager.CreateAsync(user,registerDto.Password);
            if (!createRes.Succeeded)
            {
                return new Responce<string>(HttpStatusCode.BadRequest,"Your password is incorrect");
            }
            return new Responce<string>(HttpStatusCode.OK, "User registration successful");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private string GenerateJwtToken(int userId, string name, string email, System.Collections.Generic.IList<string> roles,string phone)
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
            new (JwtRegisteredClaimNames.PhoneNumber,phone)
        };
        foreach (var r in roles)
        {
            claims.Add(new Claim("role", r));
        }

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


