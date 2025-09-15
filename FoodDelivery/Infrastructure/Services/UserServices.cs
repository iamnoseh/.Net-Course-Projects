using System.Net;
using Domain.DTOs.UserDtos;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Infrastructure.Services;

public class UserServices(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager):IUserServices
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

    public async Task<Responce<string>> CreateUser(CreateUserDto user)
    {
        try
        {
            var existing = await _userManager.FindByEmailAsync(user.Email);
            if (existing != null)
            {
                return new Responce<string>(HttpStatusCode.Conflict, "Email already registered");
            }
            var newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                UserName = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                RegistrationDate = user.RegistrationDate == default ? DateTime.UtcNow : user.RegistrationDate,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            var createRes = await _userManager.CreateAsync(newUser, user.Password);
            if (!createRes.Succeeded)
            {
                var message = string.Join("; ", createRes.Errors.Select(e => e.Description));
                return new Responce<string>(HttpStatusCode.BadRequest, message);
            }

            return new Responce<string>(HttpStatusCode.Created, "User created");
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
            var update = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == user.Id);
            if(update == null){return new Responce<string>(HttpStatusCode.NotFound,"User not found");}
            update.Name = user.Name;
            update.Email = user.Email;
            update.UserName = user.Email;
            update.Phone = user.Phone;
            update.Address = user.Address;
            update.UpdateDate = DateTime.UtcNow;

            var updateRes = await _userManager.UpdateAsync(update);
            if (!updateRes.Succeeded)
            {
                var message = string.Join("; ", updateRes.Errors.Select(e => e.Description));
                return new Responce<string>(HttpStatusCode.BadRequest, message);
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(update);
                var passRes = await _userManager.ResetPasswordAsync(update, token, user.Password);
                if (!passRes.Succeeded)
                {
                    var msg = string.Join("; ", passRes.Errors.Select(e => e.Description));
                    return new Responce<string>(HttpStatusCode.BadRequest, msg);
                }
            }

            return new Responce<string>(HttpStatusCode.OK,"User updated");
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
            var delete = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(delete == null){return new Responce<string>(HttpStatusCode.NotFound,"User not found");}
            var res = await _userManager.DeleteAsync(delete);
            return res.Succeeded
                ? new Responce<string>(HttpStatusCode.OK,"User deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,string.Join("; ", res.Errors.Select(e=>e.Description)));
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
            var users = await _userManager.Users.ToListAsync();
            if(users.Count == 0){return new Responce<List<GetUserDto>>(HttpStatusCode.NotFound,"User not found");}

            var dto = users.Select(x => new GetUserDto()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                RegistrationDate = x.RegistrationDate,
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
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(user == null) { return new Responce<GetUserDto>(HttpStatusCode.NotFound,"User not found"); }

            var dto = new GetUserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                RegistrationDate = user.RegistrationDate,
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