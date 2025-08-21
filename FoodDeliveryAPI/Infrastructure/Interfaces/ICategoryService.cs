using Domain.DTOs.CtegoriesDto;
using Domain.Entities;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Responce<string>> AddCategory(CreateCategoriesDto category);
    Task<Responce<string>> UpdateCategory(UpdateCategoriesDto category);
    Task<Responce<string>> DeleteCategory(int Id);
    Task<Responce<List<GetCategoriesDto>>> ListCategories();
    Task<Responce<GetCategoriesDto>> FindCategory(int Id);
}