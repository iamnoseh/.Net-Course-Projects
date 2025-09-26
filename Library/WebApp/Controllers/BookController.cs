using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services.Book;
using Domain.Dtos.Book;
using Domain.Responces;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Response<List<GetBookDto>>>> GetBooks()
    {
        var result = await bookService.GetBooksAsync();
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GetBookDto>>> GetBook(int id)
    {
        var result = await bookService.GetBookByIdAsync(id);
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<Response<string>>> CreateBook([FromBody] CreateBookDto dto)
    {
        var result = await bookService.CreateBookAsync(dto);
        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<string>>> UpdateBook(int id, [FromBody] UpdateBookDto dto)
    {
        var result = await bookService.UpdateBookAsync(dto);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<string>>> DeleteBook(int id)
    {
        var result = await bookService.DeleteBookAsync(id);
        return Ok(result);
    }
}