using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderAsync(int orderId);
    Task<IEnumerable<OrderItem>> GetOrderItemsByProductAsync(int productId);
}

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderAsync(int orderId)
    {
        return await _dbSet
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByProductAsync(int productId)
    {
        return await _dbSet
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .Where(oi => oi.ProductId == productId)
            .ToListAsync();
    }
}
