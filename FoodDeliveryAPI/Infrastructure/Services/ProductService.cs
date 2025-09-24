using System.Net;
using Domain.DTOs.ProductDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Responce;
using Infrastructure.Responces;

namespace Infrastructure.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<Responce<string>> AddProduct(CreateProductDto product)
    {
        try
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            
            await unitOfWork.Products.AddAsync(newProduct);
            var res = await unitOfWork.SaveChangesAsync();
            
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created, "Product added successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Product not added");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<string>> UpdateProduct(UpdateProductDto product)
    {
        try
        {
            var oldProduct = await unitOfWork.Products.GetByIdAsync(product.Id);
            if (oldProduct == null) { return new Responce<string>(HttpStatusCode.NotFound, "Product not found"); }
            
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.IsAvailable = product.IsAvailable;
            oldProduct.UpdateDate = DateTime.UtcNow;
            
            await unitOfWork.Products.UpdateAsync(oldProduct);
            var res = await unitOfWork.SaveChangesAsync();
            
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK, "Product updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Product not updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<string>> DeleteProduct(int Id)
    {
        try
        {
            var product = await unitOfWork.Products.GetByIdAsync(Id);
            if (product == null) { return new Responce<string>(HttpStatusCode.NotFound, "Product not found"); }
            
            await unitOfWork.Products.DeleteAsync(product);
            var res = await unitOfWork.SaveChangesAsync();
            
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK, "Product deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Product not deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<List<GetProductDto>>> ListProducts()
    {
        try
        {
            var products = await unitOfWork.Products.GetAllAsync();
            if (products.Count() == 0) { return new Responce<List<GetProductDto>>(HttpStatusCode.NotFound, "Product not found"); }
            
            var dto = products.Select(x => new GetProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            
            return new Responce<List<GetProductDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetProductDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<GetProductDto>> FindProduct(int Id)
    {
        try
        {
            var product = await unitOfWork.Products.GetByIdAsync(Id);
            if (product == null) { return new Responce<GetProductDto>(HttpStatusCode.NotFound, "Product not found"); }

            var dto = new GetProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable,
                CreateDate = product.CreateDate,
                UpdateDate = product.UpdateDate
            };
            return new Responce<GetProductDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Responce<List<GetProductDto>>> FindProductsByCategories(int categoryId)
    {
        try
        {
            var products = await unitOfWork.Products.FindAsync(x => x.CategoryId == categoryId);
            if (products.Count() == 0) { return new Responce<List<GetProductDto>>(HttpStatusCode.NotFound, "Product not found"); }

            var dto = products.Select(x => new GetProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetProductDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetProductDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<PaginationResponse<List<GetProductDto>>> GetProductFilters(ProductFilter filter)
    {
        try
        {
            var products = await unitOfWork.Products.GetAllAsync();
            var query = products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                // Note: This would need to be implemented with proper Include in repository
                query = query.Where(x => x.CategoryId != 0); // Simplified for now
            }

            if (filter.MaxPrice.HasValue && filter.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= filter.MinPrice && x.Price <= filter.MaxPrice);
            }

            if (filter.IsAvailable.HasValue)
            {
                query = query.Where(x => x.IsAvailable == filter.IsAvailable.Value);
            }

            var totalRecords = query.Count();
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            var pagedProducts = query.Skip(skip).Take(filter.PageSize).ToList();

            var dto = pagedProducts.Select(x => new GetProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();

            return new PaginationResponse<List<GetProductDto>>(dto, totalRecords, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PaginationResponse<List<GetProductDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
}