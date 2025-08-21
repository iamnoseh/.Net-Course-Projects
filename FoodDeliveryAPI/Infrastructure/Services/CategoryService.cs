using System.Net;
using Domain.DTOs.CtegoriesDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context): ICategoryService
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
            var res=  await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Category added successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Category not found");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<string>> UpdateCategory(UpdateCategoriesDto category)
    {
        try
        {
            var categoryToUpdate = await context.Categories.FirstOrDefaultAsync(x=>x.Id==category.Id);
            if(categoryToUpdate==null){return new Responce<string>(HttpStatusCode.NotFound,"Category not found");}
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.UpdateDate=DateTime.UtcNow;
            var res= await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Category updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Category not found");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<string>> DeleteCategory(int Id)
    {
        try
        {
            var res= await context.Categories.FirstOrDefaultAsync(x=>x.Id==Id);
            if(res==null){return new Responce<string>(HttpStatusCode.NotFound,"Category not found");}
            context.Categories.Remove(res);
            var effect=await context.SaveChangesAsync();
            return effect > 0
                ? new Responce<string>(HttpStatusCode.OK,"Category deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Category not found");

        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<List<GetCategoriesDto>>> ListCategories()
    {
        try
        {
            var res= await context.Categories.ToListAsync();
            if(res.Count==0){return new Responce<List<GetCategoriesDto>>(HttpStatusCode.NotFound,"Category not found");}

            var dto = res.Select(c => new GetCategoriesDto()
            {
                Id = c.Id,
                Name = c.Name,
                CreateDate = c.CreateDate,
                UpdateDate = c.UpdateDate
            }).ToList();
            return new Responce<List<GetCategoriesDto>>(dto);

        }
        catch (Exception e)
        {
            return new Responce<List<GetCategoriesDto>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<GetCategoriesDto>> FindCategory(int Id)
    {
        try
        {
            var res= await context.Categories.FirstOrDefaultAsync(x=>x.Id==Id);
            if(res==null){return new Responce<GetCategoriesDto>(HttpStatusCode.NotFound,"Category not found");}

            var dto = new GetCategoriesDto()
            {
                Id = res.Id,
                Name = res.Name,
                CreateDate = res.CreateDate,
                UpdateDate = res.UpdateDate
            };
            return new Responce<GetCategoriesDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetCategoriesDto>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }
}