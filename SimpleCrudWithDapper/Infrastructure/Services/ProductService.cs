using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class ProductService(DataContext context) : IProductService
{
    public async Task<Response<string>> CreateProduct(Product product)
    {
        await using var connect = context.GetConnection();
        const string query = @"Insert into products(name, price, createdAt) 
        values (@Name,@Price,@createdAt)";
        var effect = await connect.ExecuteAsync(query, new
        {
            Name = product.Name,
            Price = product.Price,
            CreatedAt = DateTime.UtcNow
        });
        return effect > 0 
            ? new Response<string>(HttpStatusCode.Created,"Product created successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Response<string>> UpdateProduct(Product product)
    {
        await using var connect = context.GetConnection();
        const string query = @"Update products set name=@Name, price=@Price, createdAt=@CreatedAt where id=@Id";
        var effect = await connect.ExecuteAsync(query, new
        {
            Name = product.Name,
            Price = product.Price,
            CreatedAt = DateTime.UtcNow,
            Id = product.Id
        });
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK,"Product updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Response<string>> DeleteProduct(int id)
    {
        await using var connect = context.GetConnection();
        const string query = @"Delete products where id=@Id";
        var effect = await connect.ExecuteAsync(query, new { Id = id });
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK,"Product deleted Successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Response<List<Product>>> GetAllProducts()
    {
        await using var connect = context.GetConnection();
        const string query = @"Select * from products";
        var effect = await connect.QueryAsync<Product>(query);
        return effect.Any() 
            ? new Response<List<Product>>(effect.ToList()) 
            : new Response<List<Product>>(HttpStatusCode.NotFound,"No products found");
    }

    public async Task<Response<Product>> GetProduct(int id)
    {
        await using var connect = context.GetConnection();
        const string query = @"Select * from products where id=@Id";
        var effect = await connect.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        return effect != null
            ? new Response<Product>(effect) 
            : new Response<Product>(HttpStatusCode.NotFound,"Product not found");
    }
}