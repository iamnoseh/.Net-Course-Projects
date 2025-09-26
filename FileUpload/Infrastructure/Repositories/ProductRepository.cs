using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories;

using Domain.Entities;

public class ProductRepository(DataContext db, IMemoryCache cache) : IProductRepository
{
    private readonly string key = "Product";
    public async Task<Product> AddAsync(Product product)
    {
        db.Products.Add(product);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
        }
        return product;
    }


    public async Task DeleteAsync(Product product)
    {
        db.Products.Remove(product);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
        }
    }


    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        if (!cache.TryGetValue(key, out List<Product>? products))
        {
            var res = await db.Products.ToListAsync();
            cache.Set(key, res, TimeSpan.FromDays(1));
            return res;
        }
        return products!;
    }


    public async Task<Product?> GetByIdAsync(Guid id)
    {
        if (!cache.TryGetValue(key, out Product? product))
        {
            var res = await db.Products.FindAsync(id);
            cache.Set(key, res, TimeSpan.FromDays(1));
            return res;
        }
        return product;
    }


    public async Task UpdateAsync(Product product)
    {
        db.Products.Update(product);
        var res = await db.SaveChangesAsync();
        if (res > 0)
        {
            cache.Remove(key);
        }
    }
}