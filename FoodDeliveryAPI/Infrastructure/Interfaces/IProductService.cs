using Domain.DTOs.ProductDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Responce;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Responce<string>> AddProduct(CreateProductDto product);
    Task<Responce<string>> UpdateProduct(UpdateProductDto product);
    Task<Responce<string>> DeleteProduct(int id);
    Task<Responce<List<GetProductDto>>> ListProducts();
    Task<Responce<GetProductDto>> FindProduct(int id);
    Task<Responce<List<GetProductDto>>> FindProductsByCategories(int categoryId);
    Task<PaginationResponse<List<GetProductDto>>> GetProductFilters(ProductFilter filter);
}