using System.Net;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IMemoryCacheService memoryCacheService,
    IRedisCacheService redisCacheService) : ICategoryService
{
    public async Task<PaginationResponse<List<GetCategoryDto>>> GetAll(CategoryFilter filter)
    {
        const string cacheKey = "categories";

        var categoriesInCache = await memoryCacheService.GetData<List<GetCategoryDto>>(cacheKey);

        if (categoriesInCache == null)
        {
            var categories = await categoryRepository.GetAll();
            categoriesInCache = categories.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            await memoryCacheService.SetData(cacheKey, categoriesInCache, 1);
        }

        if (!string.IsNullOrEmpty(filter.Name))
        {
            categoriesInCache = categoriesInCache.Where(c => c.Name.ToLower().Trim().Contains(filter.Name.ToLower().Trim())).ToList();
        }

        var totalRecords = categoriesInCache.Count;

        var paginatedData = categoriesInCache
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        return new PaginationResponse<List<GetCategoryDto>>(paginatedData, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<Response<string>> CreateCategory(AddCategoryDto request)
    {
        var category = new Category()
        {
            Name = request.Name,
            Description = request.Description
        };

        var result = await categoryRepository.CreateCategory(category);

        if (result != 1) return new Response<string>(HttpStatusCode.InternalServerError, "Failed");

        await memoryCacheService.DeleteData("categories"); 
        // await redisCacheService.RemoveData("categories");
        return new Response<string>("Success");
    }

    public async Task<Response<string>> UpdateCategory(UpdateCategoryDto request)
    {
        var category = await categoryRepository.GetCategory(c => c.Id == request.Id);

        if (category == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Category not found");
        }

        category.Name = request.Name;
        category.Description = request.Description;

        var result = await categoryRepository.UpdateCategory(category);

        if (result != 1) return new Response<string>(HttpStatusCode.BadRequest, "Failed");

        await memoryCacheService.DeleteData("categories"); 
        // await redisCacheService.RemoveData("categories");
        return new Response<string>("Success");
    }

    public async Task<Response<string>> DeleteCategory(int id)
    {
        var category = await categoryRepository.GetCategory(c => c.Id == id);
        if (category == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Category not found");
        }

        var result = await categoryRepository.DeleteCategory(category);

        if (result != 1) return new Response<string>(HttpStatusCode.BadRequest, "Failed");

        await memoryCacheService.DeleteData("categories");
        // await redisCacheService.RemoveData("categories");
        return new Response<string>("Success");
    }
}
