using System.Net;
using Domain.DTOs.CtegoriesDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context) : ICategoryService
{
    public async Task<Responce<string>> AddCategory(CreateCategoriesDto category)
    {
        try
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.Categories.AddAsync(newCategory);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Category created successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<string>> UpdateCategory(UpdateCategoriesDto category)
    {
        var oldCategory = await context.Categories.FirstOrDefaultAsync(x=> x.Id ==  category.Id);
        if (oldCategory == null) return new Responce<string>(HttpStatusCode.NotFound,"Category not found");
        oldCategory.Name = category.Name;
        oldCategory.UpdateDate = DateTime.UtcNow;
        var res = await context.SaveChangesAsync();
        return res > 0
            ?  new Responce<string>(HttpStatusCode.OK,"Category updated successfully")
            : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Responce<string>> DeleteCategory(int Id)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null) return new Responce<string>(HttpStatusCode.NotFound,"Category not found");
            context.Categories.Remove(category);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new  Responce<string>(HttpStatusCode.OK,"Category deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<List<GetCategoriesDto>>> ListCategories()
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            if(categories.Count == 0) return new Responce<List<GetCategoriesDto>>(HttpStatusCode.NotFound,"Category not found");
            var res = categories.Select(x=> new  GetCategoriesDto()
            {
                Id = x.Id,
                Name = x.Name,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetCategoriesDto>>(res);
        }
        catch (Exception e)
        {
            return new Responce<List<GetCategoriesDto>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<GetCategoriesDto>> FindCategory(int Id)
    {
        try
        {
            var category =  await context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null) return new Responce<GetCategoriesDto>(HttpStatusCode.NotFound,"Category not found");
            var res = new GetCategoriesDto()
            {
                Id = category.Id,
                Name = category.Name,
                CreateDate = category.CreateDate,
                UpdateDate = category.UpdateDate
            };
            return new Responce<GetCategoriesDto>(res);
        }
        catch (Exception e)
        {
            return new Responce<GetCategoriesDto>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }
}