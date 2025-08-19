using Domain.Entities;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public interface IProductService
{
    Task<Response<string>> CreateProduct(Product product);
    Task<Response<string>> UpdateProduct(Product product);
    Task<Response<string>> DeleteProduct(int id);
    Task<Response<List<Product>>> GetAllProducts();
    Task<Response<Product>> GetProduct(int id);
}