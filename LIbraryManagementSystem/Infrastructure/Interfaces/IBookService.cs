using Domain.DTOs;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Response<string> CreateBook(CreateBookDto request);
    Response<string> UpdateBook(UpdateBookDto request);
    Response<string> DeleteBook(int id);
    Response<List<GetBookDto>> GetAllBooks();
    Response<GetBookDto> GetBook(int id);
    Response<List<GetBookDto>> GetBooksByAuthor(int authorId);
    Response<List<GetBookDto>> GetAvailableBooks();
}