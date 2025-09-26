using System.Net;
using AutoMapper;
using Domain.Dtos.Book;
using Domain.Responces;
using Infrastructure.Repositories.Book;

namespace Infrastructure.Services.Book;

public class BookService(IBookRepository bookRepository, IMapper mapper) : IBookService
{
    public async Task<Response<string>> CreateBookAsync(CreateBookDto dto)
    {
        try
        {
            var book = mapper.Map<Domain.Entities.Book>(dto);
            var result = await bookRepository.CreateAsync(book);
            
            if (result.StatusCode != 200)
            {
                return new Response<string>((System.Net.HttpStatusCode)result.StatusCode, result.Message);
            }

            return new Response<string>("Book created");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, 
                "Something went wrong");
        }
    }

    public async Task<Response<string>> UpdateBookAsync(UpdateBookDto dto)
    {
        try
        {

            var existsResult = await bookRepository.GetByIdAsync(dto.Id);
            if (existsResult.StatusCode != 200)
            {
                return new Response<string>((System.Net.HttpStatusCode)existsResult.StatusCode, existsResult.Message);
            }

            if (existsResult == null)
            {
                return new Response<string>(System.Net.HttpStatusCode.NotFound, "Book not found");
            }

            var currentBookResult = await bookRepository.GetByIdAsync(dto.Id);
            if (currentBookResult.StatusCode != 200 || currentBookResult.Data == null)
            {
                return new Response<string>((HttpStatusCode)currentBookResult.StatusCode, currentBookResult.Message);
            }

            var book = mapper.Map<Domain.Entities.Book>(dto);
            book.CreatedDate = currentBookResult.Data.CreatedDate;

            var result = await bookRepository.UpdateAsync(book);
            
            if (result.StatusCode != 200)
            {
                return new Response<string>((HttpStatusCode)result.StatusCode, result.Message);
            }

            return new Response<string>(HttpStatusCode.OK,"Book updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, 
                $"Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        try
        {
            var result = await bookRepository.DeleteAsync(id);
            
            if (result.StatusCode != 200)
            {
                return new Response<string>((System.Net.HttpStatusCode)result.StatusCode, result.Message);
            }

            return new Response<string>("Book deleted");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, 
                $"Something went wrong");
        }
    }

    public async Task<Response<GetBookDto>> GetBookByIdAsync(int id)
    {
        try
        {
            var result = await bookRepository.GetByIdAsync(id);
            
            if (result.StatusCode != 200)
            {
                return new Response<GetBookDto>((System.Net.HttpStatusCode)result.StatusCode, result.Message);
            }

            if (result.Data == null)
            {
                return new Response<GetBookDto>(System.Net.HttpStatusCode.NotFound, "Book not found");
            }

            var bookDto = mapper.Map<GetBookDto>(result.Data);
            return new Response<GetBookDto>(bookDto);
        }
        catch (Exception ex)
        {
            return new Response<GetBookDto>(System.Net.HttpStatusCode.InternalServerError, 
                $"Something went wrong");
        }
    }

    public async Task<Response<List<GetBookDto>>> GetBooksAsync()
    {
        try
        {
            var result = await bookRepository.GetAllAsync();
            
            if (result.StatusCode != 200)
            {
                return new Response<List<GetBookDto>>((System.Net.HttpStatusCode)result.StatusCode, result.Message);
            }

            var bookDtos = mapper.Map<List<GetBookDto>>(result.Data);
            return new Response<List<GetBookDto>>(bookDtos);
        }
        catch (Exception ex)
        {
            return new Response<List<GetBookDto>>(System.Net.HttpStatusCode.InternalServerError, 
                $"Something went wrong");
        }
    }
}