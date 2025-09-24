using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetOrdersByCourierAsync(int courierId);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(Domain.Enums.Status status);
    Task<Order?> GetOrderWithItemsAsync(int orderId);
}

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.Courier)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCourierAsync(int courierId)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.Courier)
            .Where(o => o.CourierId == courierId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(Domain.Enums.Status status)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.Courier)
            .Where(o => o.Status == status)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsAsync(int orderId)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.Courier)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
