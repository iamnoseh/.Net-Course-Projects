using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Seeder;

public static class IdentitySeeder
{
    public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        var adminEmail = configuration["AdminSeed:Email"];
        var adminPassword = configuration["AdminSeed:Password"];
        var adminName = configuration["AdminSeed:Name"] ?? "Administrator";
        var adminPhone = configuration["AdminSeed:Phone"];
        var adminAddress = configuration["AdminSeed:Address"] ?? "System";

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            return;
        }

        var adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(adminRole));
        }

        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            user = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = adminName,
                Phone = adminPhone,
                Address = adminAddress,
                RegistrationDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            var createRes = await userManager.CreateAsync(user, adminPassword);
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
}


