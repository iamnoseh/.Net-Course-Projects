using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

using Domain.Entities;

public class ProductRepository(DataContext db) : IProductRepository
{
    public async Task<Product> AddAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product;
    }


    public async Task DeleteAsync(Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
    }


    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await db.Products.AsNoTracking().ToListAsync();
    }


    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await db.Products.FindAsync(id);
    }


    public async Task UpdateAsync(Product product)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync();
    }
}
