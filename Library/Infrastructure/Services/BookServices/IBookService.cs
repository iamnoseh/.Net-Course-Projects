using Domain.Dtos.Book;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Services.BookServices;

public interface IBookService
{
    Task<Response<string>> CreateBook(CreateBookDto dto);
    Task<Response<string>> UpdateBook(UpdateBookDto dto);
    Task<Response<string>> DeleteBook(int id);
    Task<Response<List<Book>>> GetBooks();
    Task<Response<Book?>> GetBook(int id);
}