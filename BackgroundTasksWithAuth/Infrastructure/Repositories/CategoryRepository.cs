using System.Linq.Expressions;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class CategoryRepository(DataContext context, ILogger<CategoryRepository> logger) : ICategoryRepository
{
    public Task<IQueryable<Category>> GetAll()
    {
        var query = context.Categories.AsQueryable();
        return Task.FromResult(query);
    }

    public async Task<Category?> GetCategory(Expression<Func<Category, bool>>? filter)
    {
        var query = context.Categories.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CreateCategory(Category request)
    {
        try
        {
            await context.Categories.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> UpdateCategory(Category request)
    {
        try
        {
            context.Categories.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> DeleteCategory(Category request)
    {
        try
        {
            context.Categories.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}
