using System.Net;
using Domain.DTOs.UserDtos;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Infrastructure.Services;

public class UserServices(DataContext context):IUserServices
{
    private readonly DataContext _context = context;

    public async Task<Responce<string>> CreateUser(CreateUserDto user)
    {
        try
        {
            // enforce unique email
            var existing = await context.Users.AnyAsync(u => u.Email == user.Email);
            if (existing)
            {
                return new Responce<string>(HttpStatusCode.Conflict, "Email already registered");
            }

            var newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Address = user.Address,
                RegistrationDate = user.RegistrationDate,
                Role = user.Role,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.Users.AddAsync(newUser);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"User created")
                : new Responce<string>(HttpStatusCode.BadRequest,"User not created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> UpdateUser(UpdateUserDto user)
    {
        try
        {
            var update = await context.Users.FirstOrDefaultAsync(x=>x.Id == user.Id);
            if(update == null){return new Responce<string>(HttpStatusCode.NotFound,"User not found");}
            update.Name = user.Name;
            update.Email = user.Email;
            update.Phone = user.Phone;
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                update.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            update.Address = user.Address;
            update.Role= user.Role;
            update.UpdateDate = DateTime.UtcNow;
            var res =  await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"User updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"User not updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<string>> DeleteUser(int Id)
    {
        try
        {
            var delete = await context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(delete == null){return new Responce<string>(HttpStatusCode.NotFound,"User not found");}
            context.Users.Remove(delete);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"User deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"User not deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<List<GetUserDto>>> GetUsers()
    {
        try
        {
            var users = await context.Users.ToListAsync();
            if(users.Count == 0){return new Responce<List<GetUserDto>>(HttpStatusCode.NotFound,"User not found");}

            var dto = users.Select(x => new GetUserDto()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                RegistrationDate = x.RegistrationDate,
                Role = x.Role,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetUserDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetUserDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<GetUserDto>> GetUserById(int Id)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(user == null) { return new Responce<GetUserDto>(HttpStatusCode.NotFound,"User not found"); }

            var dto = new GetUserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                RegistrationDate = user.RegistrationDate,
                Role = user.Role,
                CreateDate = user.CreateDate,
                UpdateDate = user.UpdateDate
            };
            return new Responce<GetUserDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetUserDto>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
}