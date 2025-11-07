using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories;

using Domain.Entities;

public class ProductRepository(DataContext db, IMemoryCache cache) : IProductRepository
{
    private readonly string key = "Product";

    public async Task<int> AddAsync(Product product)
    {
        await db.Products.AddAsync(product);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
            var pr = await db.Products.ToListAsync();
            cache.Set(key, pr, TimeSpan.FromDays(20));
        }
        return res;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        if (!cache.TryGetValue(key, out List<Product> products))
        {
            var res = await db.Products.FindAsync(id);
            return res;
        }
        return products.FirstOrDefault(x=> x.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        if (!cache.TryGetValue(key, out List<Product> products))
        {
            var res =  await db.Products.ToListAsync();
            cache.Set(key, res, TimeSpan.FromDays(20));
            return res;
        }
        return products;
    }

    public async Task<int> UpdateAsync(Product product)
    {
        db.Products.Update(product);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
            var pr =  await db.Products.ToListAsync();
            cache.Set(key, pr, TimeSpan.FromDays(20));
        }
        return res;
    }

    public async Task<int> DeleteAsync(Product product)
    {
        var product1 = await db.Products.FindAsync(product.Id);
        db.Products.Remove(product1);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
            var pr =  await db.Products.ToListAsync();
            cache.Set(key, pr, TimeSpan.FromDays(20));
        }
        return res;
    }
}