using Domain.DTOs.UserDtos;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface IUserServices
{
    Task<Responce<string>> CreateUser(CreateUserDto user);
    Task<Responce<string>> UpdateUser(UpdateUserDto user);
    Task<Responce<string>> DeleteUser(int Id);
    Task<Responce<List<GetUserDto>>> GetUsers();
    Task<Responce<GetUserDto>> GetUserById(int Id);
}