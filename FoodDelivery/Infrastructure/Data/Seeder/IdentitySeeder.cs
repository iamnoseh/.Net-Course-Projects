using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Seeder;

public static class IdentitySeeder
{
    public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        var adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(adminRole));
        }

        var user = await userManager.FindByNameAsync("admin123");
        if (user == null)
        {
            user = new User
            {
                UserName = "admin123",
                Email = "admin@mail.ru",
                Name = "admin",
                Phone = "987654321",
                Address = "Demo",
                RegistrationDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            var createRes = await userManager.CreateAsync(user, "Qwerty123.");
            if (!createRes.Succeeded)
            {
                return;
            }
        }

        var roles = await userManager.GetRolesAsync(user);
        if (!roles.Contains(adminRole))
        {
            await userManager.AddToRoleAsync(user, adminRole);
        }
    }

    public static async Task<bool> SeedRole(RoleManager<IdentityRole<int>> roleManage)
    {
        var newRoles = new List<IdentityRole<int>>()
        {
            new(UserRole.Admin.ToString()),
            new(UserRole.Client.ToString()),
            new(UserRole.Courier.ToString()),
        };
        var roles = await roleManage.Roles.ToListAsync(); 
        foreach (var role in newRoles)
        {
            if (roles.Any(e => e.Name == role.Name))
                continue;
            await roleManage.CreateAsync(role);
        }
        return true;
    }
}