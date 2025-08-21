using Domain.DTOs.CtegoriesDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service):Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await service.ListCategories();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindCategory(int id)
    {
        var res = await service.FindCategory(id);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoriesDto category)
    {
        var res = await service.AddCategory(category);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoriesDto category)
    {
        var res = await service.UpdateCategory(category);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var res = await service.DeleteCategory(id);
        return Ok(res);
    }
}