using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.BackgroundTasks;

public class DeleteNullCategories(IServiceScopeFactory serviceScopeFactory,IMemoryCache cache) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var deletedCategories =
                await dbContext.Categories.Where(x => x.Description == null).ToListAsync(stoppingToken);
            if (deletedCategories.Count > 0)
            {
                dbContext.Categories.RemoveRange(deletedCategories);
                await dbContext.SaveChangesAsync(stoppingToken);
                cache.Remove("categories");
            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}