using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public interface ICourierRepository : IRepository<Courier>
{
    Task<IEnumerable<Courier>> GetAvailableCouriersAsync();
    Task<Courier?> GetCourierWithOrdersAsync(int courierId);
}

public class CourierRepository : Repository<Courier>, ICourierRepository
{
    public CourierRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Courier>> GetAvailableCouriersAsync()
    {
        return await _dbSet
            .Include(c => c.User)
            .Where(c => c.IsAvailable)
            .ToListAsync();
    }

    public async Task<Courier?> GetCourierWithOrdersAsync(int courierId)
    {
        return await _dbSet
            .Include(c => c.User)
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == courierId);
    }
}
