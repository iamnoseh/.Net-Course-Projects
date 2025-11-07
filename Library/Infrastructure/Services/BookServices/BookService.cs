using System.Net;
using AutoMapper;
using Domain.Dtos.Book;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Repositories.Book;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.Services.BookServices;

public class BookService(IBookRepository repository,IMapper map) : IBookService
{
    public async Task<Response<string>> CreateBook(CreateBookDto dto)
    {
        var newBook = map.Map<Book>(dto);
        Log.Information("Creating new book");
        var res = await repository.CreateBook(newBook);
        if(res > 0) {Log.Information("Book created successfully");}
        else {Log.Warning("Failed to create new book");}
        return res > 0 
            ? new Response<string>(HttpStatusCode.Created,"Book created successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Book could not be created");
    }

    public async Task<Response<string>> UpdateBook(UpdateBookDto dto)
    {

        var book = await repository.GetBook(dto.Id);
        var newBook = new Book()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            CreatedDate = book.CreatedDate,
            IsPublic = book.IsPublic,
            PublishDate = dto.PublishDate,
            UpdatedDate = DateTime.UtcNow
        };
        var res = await repository.UpdateBook(newBook);
        return res > 0
            ? new Response<string>(HttpStatusCode.OK,"Book updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Book could not be updated");
    }

    public async Task<Response<string>> DeleteBook(int id)
    {
        var res = await repository.GetBook(id);
        if (res == null) return new Response<string>(HttpStatusCode.NotFound,"Book not found");
        res.IsDeleted = true;
        var effect = await repository.UpdateBook(res);
        return effect > 0
            ?  new Response<string>(HttpStatusCode.OK,"Book deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest,"Book could not be deleted");
    }

    public async Task<Response<List<Book>>> GetBooks()
    {
        var res = await repository.GetBooks();
        return new  Response<List<Book>>(res);
    }

    public async Task<Response<Book?>> GetBook(int id)
    {
        var res = await repository.GetBook(id);
        return new Response<Book?>(res);
    }
}