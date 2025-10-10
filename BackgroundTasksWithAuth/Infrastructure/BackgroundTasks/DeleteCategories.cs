using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundTasks;

public class DeleteCategories(IServiceScopeFactory  scopeFactory)
{
    public async Task DeleteCategoriesWhereNotDescription()
    {
        using var scope  = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        var categories = await db.Categories.Where(x => x.Description == null).ToListAsync();
        if(categories.Count == 0) return;
        db.Categories.RemoveRange(categories);
        await db.SaveChangesAsync();
    }
    
}