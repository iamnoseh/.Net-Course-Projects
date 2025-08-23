using Domain.DTOs.ProductDto;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service):Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await service.ListProducts();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await service.FindProduct(id);
        return Ok(res);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetCategory([FromRoute]int categoryId)
    {
        var res = await service.FindProductsByCategories(categoryId);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductDto dto)
    {
        var res= await service.AddProduct(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateProductDto dto)
    {
        var res= await service.UpdateProduct(dto);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res= await service.DeleteProduct(id);
        return Ok(res);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> GetPagination([FromQuery] ProductFilter filter)
    {
        var res = await service.GetProductFilters(filter);
        return Ok(res);
    }
    
    
}