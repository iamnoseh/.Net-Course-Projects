using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DataContext>();


builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped(sp =>
    new ProductService(
        sp.GetRequiredService<IProductRepository>(),
        sp.GetRequiredService<IFileStorageService>()
    )
);
builder.Services.AddScoped<IFileStorageService>(
    sp => new FileStorageService(builder.Environment.ContentRootPath));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.MapControllers();
app.Run();