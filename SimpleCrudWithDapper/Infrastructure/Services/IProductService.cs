using Domain.Entities;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public interface IProductService
{
    Response<string> CreateProduct(Product product);
    Response<string> UpdateProduct(Product product);
    Response<string> DeleteProduct(int id);
    Response<List<Product>> GetAllProducts();
    Response<Product> GetProductById(int id);
}