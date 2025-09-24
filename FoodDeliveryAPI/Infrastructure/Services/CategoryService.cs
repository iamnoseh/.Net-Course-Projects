using System.Net;
using Domain.DTOs.CtegoriesDto;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Responce;

namespace Infrastructure.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
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
            
            await unitOfWork.Categories.AddAsync(newCategory);
            var res = await unitOfWork.SaveChangesAsync();
            
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created, "Category added successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Category not found");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Responce<string>> UpdateCategory(UpdateCategoriesDto category)
    {
        try
        {
            var categoryToUpdate = await unitOfWork.Categories.GetByIdAsync(category.Id);
            if (categoryToUpdate == null) { return new Responce<string>(HttpStatusCode.NotFound, "Category not found"); }
            
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.UpdateDate = DateTime.UtcNow;
            
            await unitOfWork.Categories.UpdateAsync(categoryToUpdate);
            var res = await unitOfWork.SaveChangesAsync();
            
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK, "Category updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Category not found");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Responce<string>> DeleteCategory(int Id)
    {
        try
        {
            var category = await unitOfWork.Categories.GetByIdAsync(Id);
            if (category == null) { return new Responce<string>(HttpStatusCode.NotFound, "Category not found"); }
            
            await unitOfWork.Categories.DeleteAsync(category);
            var effect = await unitOfWork.SaveChangesAsync();
            
            return effect > 0
                ? new Responce<string>(HttpStatusCode.OK, "Category deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Category not found");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Responce<List<GetCategoriesDto>>> ListCategories()
    {
        try
        {
            var categories = await unitOfWork.Categories.GetAllAsync();
            if (categories.Count() == 0) { return new Responce<List<GetCategoriesDto>>(HttpStatusCode.NotFound, "Category not found"); }

            var dto = categories.Select(c => new GetCategoriesDto()
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
            return new Responce<List<GetCategoriesDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Responce<GetCategoriesDto>> FindCategory(int Id)
    {
        try
        {
            var category = await unitOfWork.Categories.GetByIdAsync(Id);
            if (category == null) { return new Responce<GetCategoriesDto>(HttpStatusCode.NotFound, "Category not found"); }

            var dto = new GetCategoriesDto()
            {
                Id = category.Id,
                Name = category.Name,
                CreateDate = category.CreateDate,
                UpdateDate = category.UpdateDate
            };
            return new Responce<GetCategoriesDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetCategoriesDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}