using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Domain.Entities;
using Infrastructure.Data.Seeder;
using Infrastructure.ExtensionMethods;
using Infrastructure.Profiles;

var builder = WebApplication.CreateBuilder(args);
// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Month, fileSizeLimitBytes: 10240)
    .Enrich.FromLogContext()
    .MinimumLevel.Debug());
//datacontext
builder.Services.RegisterDataContext(builder.Configuration);
//services
builder.Services.RegisterServices();
//swagger+jwt
builder.Services.RegisterSwagger();
//idetityUser
builder.Services.AddIdentityUser();
builder.Services.AddAutoMapper(typeof(AppProfile));
// JWT Auth
builder.Services.RegisterJwt(builder.Configuration);

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
// Seed admin user and role
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        await IdentitySeeder.SeedAdminAsync(userManager, roleManager);
        await IdentitySeeder.SeedRole(roleManager);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error seeding identity data");
    }
}
app.Run();
