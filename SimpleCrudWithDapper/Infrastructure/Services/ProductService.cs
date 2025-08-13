using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class ProductService(DataContext context) : IProductService
{
    public Response<string> CreateProduct(Product product)
    {
        try
        {
            using var command = context.GetConnection();
            const string query = @"Insert into Products(name,price,createdat) values(@name, @price, @createdat)";
            var res = command.Execute(query, new
            {
                name = product.Name,
                price = product.Price,
                createdat = DateTime.Now
            });
            return res > 0 
                ? new Response<string>(HttpStatusCode.Created,"Product created successfully") 
                : new Response<string>(HttpStatusCode.BadRequest,"Error creating product");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public Response<string> UpdateProduct(Product product)
    {
        try
        {
            using var command = context.GetConnection();
            const string query = @"Update products set name=@name, price=@price, createdat=@createdat where id=@id";
            var res = command.Execute(query, new
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
                createdat = product.CreatedAt
            });
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Product updated successfully")
                : new Response<string>(HttpStatusCode.BadRequest,"Something went wrong");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public Response<string> DeleteProduct(int id)
    {
        try
        {
            using var command = context.GetConnection();
            const string query = @"Delete products where id=@id";
            var res = command.Execute(query, new
            {
                id = id
            });
            return res > 0 
                ? new Response<string>(HttpStatusCode.OK,"Product deleted successfully")
                : new Response<string>(HttpStatusCode.BadRequest,"Something went wrong");

        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public Response<List<Product>> GetAllProducts()
    {
        try
        {
            using var command = context.GetConnection();
            const string query = @"Select * from Products";
            var res = command.Query<Product>(query).ToList();
            if (res.Any())
            {
                return new Response<List<Product>>(res);
            }
            return new Response<List<Product>>(HttpStatusCode.NotFound,"Products not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return   new Response<List<Product>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public Response<Product> GetProductById(int id)
    {
        try
        {
            using var command = context.GetConnection();
            const string query = @"Select * from Products where id=@Id";
            var res = command.QueryFirst<Product>(query, new
            {
                Id = id
            });
            if (res != null)
            {
                return new Response<Product>(res);
            }
            return new Response<Product>(HttpStatusCode.NotFound,"Product not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Response<Product>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
}