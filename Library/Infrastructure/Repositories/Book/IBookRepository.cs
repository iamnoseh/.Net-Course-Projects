namespace Infrastructure.Repositories.Book;

public interface IBookRepository
{
    Task<int> CreateBook(Domain.Entities.Book? book);
    Task<int> UpdateBook(Domain.Entities.Book? book);
    Task<List<Domain.Entities.Book?>> GetBooks();
    Task<Domain.Entities.Book?> GetBook(int id);
}