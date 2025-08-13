using Domain.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WepApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService service):  ControllerBase
{
    [HttpPost]
    public Response<string> CreateBook(CreateBookDto request)
    {
        return service.CreateBook(request);
    }

    [HttpPut("{id}")]
    public Response<string> UpdateBook(int id, UpdateBookDto request)
    {
        request.Id = id;
        return service.UpdateBook(request);
    }

    [HttpDelete("{id}")]
    public Response<string> DeleteBook(int id)
    {
        return service.DeleteBook(id);
    }

    [HttpGet]
    public Response<List<GetBookDto>> GetAllBooks()
    {
        return service.GetAllBooks();
    }

    [HttpGet("{id}")]
    public Response<GetBookDto> GetBook(int id)
    {
        return service.GetBook(id);
    }

    [HttpGet("by-author/{authorId}")]
    public Response<List<GetBookDto>> GetByAuthor(int authorId)
    {
        return service.GetBooksByAuthor(authorId);
    }

    [HttpGet("available")]
    public Response<List<GetBookDto>> GetAvailable()
    {
        return service.GetAvailableBooks();
    }
}