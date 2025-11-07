using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ExtensionMethods;

public static class ServicesRegister
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<DataContext>();
        services.AddScoped<ICourierServices, CourierServices>();
        services.AddScoped<IMenuServices, MenuServices>();
        services.AddScoped<IOrderServices, OrderServices>();
        services.AddScoped<IOrderDetailServices, OrderDetailServices>();
        services.AddScoped<IRestourantServices, ResturantServices>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IAuthService, AuthService>(); 
    }
}