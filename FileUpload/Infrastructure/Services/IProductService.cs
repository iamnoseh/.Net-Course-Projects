using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Services;

public interface IProductService
{
    Task<string> CreateProduct(CreateProductDto product);
    Task<string>  UpdateProduct(UpdateProductDto product);
    Task<string>  DeleteProduct(Guid id);
    Task<GetProductDto> GetProduct(Guid id);
    Task<List<GetProductDto>> GetProducts();
}