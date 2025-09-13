using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;

namespace Infrastructure.Interfaces;

public interface IAuthService
{
    Task<Responce<string>> Login(LoginDto loginDto);
    Task<Responce<string>> Register(CreateUserDto registerDto);
}


