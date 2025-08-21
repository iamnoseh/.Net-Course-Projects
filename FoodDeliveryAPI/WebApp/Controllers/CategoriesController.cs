using Domain.DTOs.CtegoriesDto;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service):ControllerBase
{
    [HttpPost]
    public async Task<Responce<string>> AddCategory(CreateCategoriesDto category)
    {
        return await service.AddCategory(category);
    }

    [HttpPut]
    public async Task<Responce<string>> UpdateCategory(UpdateCategoriesDto category)
    {
        return await service.UpdateCategory(category);
    }

     [HttpDelete]
    public async Task<Responce<string>> DeleteCategory(int id)
    {
        return await service.DeleteCategory(id);
    }

    [HttpGet]
    public async Task<Responce<List<GetCategoriesDto>>> GetCategories()
    {
        return await service.ListCategories();
    }

    [HttpGet("{id}")]
    public async Task<Responce<GetCategoriesDto>> GetCategory(int id)
    {
        return await service.FindCategory(id);
    }
}