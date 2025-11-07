using Domain.Dtos.Book;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Services.BookServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController (IBookService service):  ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> PostBook(CreateBookDto book)
    {
        return await service.CreateBook(book);
    }
    
    [HttpGet]
    public async Task<Response<List<Book>>> GetBooks()
    {
        return await service.GetBooks();
    }
    
    [HttpGet("id")]
    public async Task<Response<Book?>> GetBook(int id)
    {
        return await service.GetBook(id);
    }

    [HttpPut]
    public async Task<Response<string>> PutBook(UpdateBookDto book)
    {
        return await service.UpdateBook(book);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteBook(int id)
    {
        return await service.DeleteBook(id);
    }
    
}