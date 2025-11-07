using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IProductRepository
{
    Task<int> AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task<int> UpdateAsync(Product product);
    Task<int> DeleteAsync(Product product);
}