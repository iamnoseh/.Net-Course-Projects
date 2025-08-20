using Domain.DTOs.ProductDto;
using Domain.Entities;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface IProductServices
{
    Task<Responce<string>> AddProduct(CreateProductDto product);
    Task<Responce<string>> UpdateProduct(UpdateProductDto product);
    Task<Responce<string>> DeleteProduct(int Id);
    Task<Responce<List<GetProductDto>>> ListProducts();
    Task<Responce<GetProductDto>> FindProduct(int Id);
    Task<Responce<List<GetProductDto>>> FindProductsByCategories(int categoryId);
}