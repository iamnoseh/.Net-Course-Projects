using System.Net;
using Domain.DTOs.ProductDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responce;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context):IProductService
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
            await context.Products.AddAsync(newProduct);
            var res = await context.SaveChangesAsync();
            return res>0
                ? new Responce<string>(HttpStatusCode.Created,"Product added successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Product not added");
            
        }
        catch (Exception e)
        {
            return new  Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<string>> UpdateProduct(UpdateProductDto product)
    {
        try
        {
            var oldProduct = await context.Products.FirstOrDefaultAsync(x=>x.Id == product.Id);
            if(oldProduct == null){return new Responce<string>(HttpStatusCode.NotFound,"Product not found");}
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.IsAvailable = product.IsAvailable;
            oldProduct.UpdateDate = DateTime.UtcNow;
            var res= await context.SaveChangesAsync();
            return res>0
                ? new Responce<string>(HttpStatusCode.OK,"Product updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Product not updated");
        }
        catch (Exception e)
        {
            return new  Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<string>> DeleteProduct(int Id)
    {
        try
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if(product == null){return new Responce<string>(HttpStatusCode.NotFound,"Product not found");}
            context.Products.Remove(product);
            var res = await context.SaveChangesAsync();
            return res>0
                ? new Responce<string>(HttpStatusCode.OK,"Product deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Product not deleted");
        }
        catch (Exception e)
        {
            return new  Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<List<GetProductDto>>> ListProducts()
    {
        try
        {
            var products = await context.Products.ToListAsync();
            if(products.Count == 0){return new Responce<List<GetProductDto>>(HttpStatusCode.NotFound,"Product not found");}
            var dto=products.Select(x=>new GetProductDto()
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
            return new Responce<List<GetProductDto>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<GetProductDto>> FindProduct(int Id)
    {
        try
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if(product == null){return new Responce<GetProductDto>(HttpStatusCode.NotFound,"Product not found");}

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
            var products = await context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (products.Count == 0) { return new Responce<List<GetProductDto>>(HttpStatusCode.NotFound, "Product not found"); }

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
            return new  Responce<List<GetProductDto>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<PaginationResponse<List<GetProductDto>>> GetProductFilters(ProductFilter filter)
    {
        try
        {
            var query = context.Products.Include(x => x.Category).AsQueryable();
            
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                query = query.Where(x => x.Category.Name.Contains(filter.CategoryName));
            }

            if (filter.Price.HasValue)
            {
                query = query.Where(x => x.Price >= filter.Price.Value);
            }

            if (filter.IsAvailable.HasValue)
            {
                query = query.Where(x => x.IsAvailable == filter.IsAvailable.Value);
            }

            var totalRecords = await query.CountAsync();
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            var products = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
            
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
            
            return new PaginationResponse<List<GetProductDto>>(dto, totalRecords, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PaginationResponse<List<GetProductDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
}