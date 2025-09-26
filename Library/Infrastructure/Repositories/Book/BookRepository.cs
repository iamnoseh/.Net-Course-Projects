using Domain.Responces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories.Book;

public class BookRepository(DataContext context,IMemoryCache cache) : IBookRepository
{
    private readonly string key = "Book"; 
    public async Task<Response<List<Domain.Entities.Book>>> GetAllAsync()
    {
        if (!cache.TryGetValue(key, out List<Domain.Entities.Book>? book))
        {
            var res = await context.Books.ToListAsync();
            cache.Set(key, res, TimeSpan.FromDays(1));
            return new Response<List<Domain.Entities.Book>>(res);
        }
        else
        {
            return new Response<List<Domain.Entities.Book>>(book);
        }
    }

    public async Task<Response<Domain.Entities.Book?>> GetByIdAsync(int id)
    {
        if (!cache.TryGetValue(key, out Domain.Entities.Book? book))
        {
            var res = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            cache.Set(key, res, TimeSpan.FromDays(1));
            return new Response<Domain.Entities.Book?>(res);
        }
        else
        {
            return new Response<Domain.Entities.Book?>(book);
        }
    }

    public async Task<Response<Domain.Entities.Book>> CreateAsync(Domain.Entities.Book book)
    {
        book.CreatedDate = DateTime.UtcNow;
        book.UpdatedDate = DateTime.UtcNow;
        context.Books.Add(book);
        var res = await context.SaveChangesAsync();
        cache.Remove(key);
        return res > 0 
        ? new Response<Domain.Entities.Book>(book)
        :  new Response<Domain.Entities.Book?>(null);
    }

    public async Task<Response<Domain.Entities.Book>> UpdateAsync(Domain.Entities.Book book)
    {
        book.UpdatedDate = DateTime.UtcNow;
        context.Books.Update(book);
        var res = await context.SaveChangesAsync();
        cache.Remove(key);
        return res > 0
         ? new Response<Domain.Entities.Book>(book)
         : new Response<Domain.Entities.Book?>(null);
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            return new Response<bool>(System.Net.HttpStatusCode.NotFound, "Китоб ёфт нашуд");
        }
        
        context.Books.Remove(book);
        var res =  await context.SaveChangesAsync();
        cache.Remove(key);
        return res > 0
            ? new Response<bool>(true)
            : new Response<bool>(false);

    }
}