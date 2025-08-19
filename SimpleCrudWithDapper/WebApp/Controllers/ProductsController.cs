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
    public async Task<Response<string>> Create(Product product)
        => await service.CreateProduct(product);
    
    [HttpPatch]
    public async Task<Response<string>> Update(Product product)
        => await service.UpdateProduct(product);
    
    [HttpDelete]
    public async Task<Response<string>> Delete(int id)
        => await service.DeleteProduct(id);

    [HttpGet]
    public async Task<Response<List<Product>>> GetProducts()
        => await service.GetAllProducts();
    [HttpGet("{id}")]
    public async Task<Response<Product>> GetProduct(int id) 
        => await service.GetProduct(id);


}