using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ProductService(IProductRepository repo, 
    IFileStorageService fileStorage) : IProductService
{
    public async Task<string> CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price
        };

        if (dto.Image != null)
        {
            product.ImagePath = await fileStorage.SaveFileAsync(dto.Image, "images");
        }

        await repo.AddAsync(product);
        return $"Product '{product.Name}' created successfully!";
    }

    public async Task<string> UpdateProduct(UpdateProductDto dto)
    {
        var product = await repo.GetByIdAsync(dto.Id);
        if (product == null)
            return "Product not found!";

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        if (dto.Image != null)
        {
            if (!string.IsNullOrEmpty(product.ImagePath))
                await fileStorage.DeleteFileAsync(product.ImagePath);

            product.ImagePath = await fileStorage.SaveFileAsync(dto.Image, "images");
        }

        await repo.UpdateAsync(product);
        return $"Product '{product.Name}' updated successfully!";
    }

    public async Task<string> DeleteProduct(Guid id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null)
            return "Product not found!";

        if (!string.IsNullOrEmpty(product.ImagePath))
            await fileStorage.DeleteFileAsync(product.ImagePath);

        await repo.DeleteAsync(product);
        return $"Product '{product.Name}' deleted successfully!";
    }

    public async Task<GetProductDto> GetProduct(Guid id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null)
            throw new Exception("Product not found!");

        return new GetProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImagePath,
            CreatedAt = product.CreatedAt
        };
    }

    public async Task<List<GetProductDto>> GetProducts()
    {
        var products = await repo.GetAllAsync();
        return products.Select(p => new GetProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImagePath,
            CreatedAt = p.CreatedAt
        }).ToList();
    }
}
