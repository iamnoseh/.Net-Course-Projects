using Domain.Entities;
using Infrastructure.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service): ControllerBase
{
    [HttpPost]
    public Response<string> Create(Product product)
    {
        return service.CreateProduct(product);
    }

    [HttpPut]
    public Response<string> Update(Product product)
    {
        return service.UpdateProduct(product);
    }

    [HttpDelete]
    public Response<string> Delete(int id)
    {
        return service.DeleteProduct(id);
    }

    [HttpGet]
    public Response<List<Product>> GetAllProducts()
    {
        return service.GetAllProducts();
    }

    [HttpGet("{id}")]
    public Response<Product> GetProduct(int id)
    {
        return service.GetProductById(id);
    }

}