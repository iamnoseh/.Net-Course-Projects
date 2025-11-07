using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Book;

public class BookRepository(DataContext context) : IBookRepository
{
    public async Task<int> CreateBook(Domain.Entities.Book? book)
    {
        await context.Books.AddAsync(book);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateBook(Domain.Entities.Book? book)
    {
        context.Books.Update(book);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Domain.Entities.Book?>> GetBooks()
    {
        return await context.Books.ToListAsync();
    }

    public async Task<Domain.Entities.Book?> GetBook(int id)
    {
        return await context.Books.FindAsync(id);
    }
}