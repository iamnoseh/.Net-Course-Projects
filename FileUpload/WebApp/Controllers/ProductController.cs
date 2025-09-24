using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
    {
        var res = await service.CreateProduct(dto);
        return Ok(res);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetProducts());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await service.GetProduct(id);
        return Ok(res);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateProductDto dto)
    {
        var res = await service.UpdateProduct(dto);
        return Ok(res);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await service.DeleteProduct(id);
        return Ok(res);
    }
}