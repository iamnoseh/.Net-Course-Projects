using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Menu>Menus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Restaurant> Restourants { get; set; }
    public DbSet<User> Users { get; set; }
}