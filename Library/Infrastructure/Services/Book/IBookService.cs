using Domain.Dtos.Book;
using Domain.Responces;

namespace Infrastructure.Services.Book;

public interface IBookService
{
    Task<Response<string>> CreateBookAsync(CreateBookDto dto);
    Task<Response<string>> UpdateBookAsync(UpdateBookDto dto);
    Task<Response<string>> DeleteBookAsync(int id);
    Task<Response<GetBookDto>> GetBookByIdAsync(int id);
    Task<Response<List<GetBookDto>>> GetBooksAsync();
}