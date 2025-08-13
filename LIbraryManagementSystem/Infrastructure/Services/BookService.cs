using System.Net;
using Dapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public Response<string> CreateBook(CreateBookDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"
            Insert into books(Title,AuthorId,Genre,PublishedYear,Price,Quantity,CreatedAt,UpdatedAt) 
            values (@title,@authorId,@genre,@publishedYear,@price,@quantity,@createdAt,@updatedAt);
            ";
            var effect = connect.Execute(query, new
            {
                title = request.Title,
                authorId = request.AuthorId,
                genre = request.Genre,
                publishedYear = request.PublishedYear,
                price = request.Price,
                quantity = request.Quantity,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.Created, "Book successfully created")
                : new Response<string>(HttpStatusCode.BadRequest, "Book could not be created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetBookDto>> GetBooksByAuthor(int authorId)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from books where authorid = @authorId;";
            var res = connect.Query<Book>(query, new { authorId }).ToList();
            List<GetBookDto> dtos = [];
            foreach (var r in res)
            {
                var dto = new GetBookDto
                {
                    Id = r.Id,
                    AuthorId = r.AuthorId,
                    Title = r.Title,
                    Genre = r.Genre,
                    Price = r.Price,
                    Quantity = r.Quantity,
                    PublishedYear = r.PublishedYear,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetBookDto>>(dtos)
                : new Response<List<GetBookDto>>(HttpStatusCode.NotFound, "Books Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetBookDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetBookDto>> GetAvailableBooks()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from books where quantity > 0;";
            var res = connect.Query<Book>(query).ToList();
            List<GetBookDto> dtos = [];
            foreach (var r in res)
            {
                var dto = new GetBookDto
                {
                    Id = r.Id,
                    AuthorId = r.AuthorId,
                    Title = r.Title,
                    Genre = r.Genre,
                    Price = r.Price,
                    Quantity = r.Quantity,
                    PublishedYear = r.PublishedYear,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetBookDto>>(dtos)
                : new Response<List<GetBookDto>>(HttpStatusCode.NotFound, "Books Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetBookDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> UpdateBook(UpdateBookDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"
            Update books set title = @title, genre = @genre,publishedyear = @publishedyear,price = @price,quantity = @quantity,UpdatedAt = @updatedAt
            where Id = @id;";
            var effect = connect.Execute(query, new
            {
                title = request.Title,
                genre = request.Genre,
                publishedYear = request.PublishedYear,
                price = request.Price,
                quantity = request.Quantity,
                updatedAt = DateTime.UtcNow,
                id = request.Id
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Book successfully updated")
                : new Response<string>(HttpStatusCode.BadRequest, "Book could not be updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> DeleteBook(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"
            Delete from books where Id = @Id;";
            var effect = connect.Execute(query, new
            {
                Id = id
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Book successfully deleted")
                : new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetBookDto>> GetAllBooks()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from books;";
            var res = connect.Query<Book>(query).ToList();
            List<GetBookDto> dtos = [];
            foreach (var r in res)
            {
                var dto = new GetBookDto()
                {
                    Id = r.Id,
                    AuthorId = r.AuthorId,
                    Title = r.Title,
                    Genre = r.Genre,
                    Price = r.Price,
                    Quantity = r.Quantity,
                    PublishedYear = r.PublishedYear,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                };
                dtos.Add(dto);
            }

            if (dtos.Any())
            {
                return new Response<List<GetBookDto>>(dtos);
            }

            return new Response<List<GetBookDto>>(HttpStatusCode.NotFound, "Books Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetBookDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<GetBookDto> GetBook(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from books where Id = @Id;";
            var effect = connect.QueryFirst<Book>(query, new { Id = id });
            var dto = new GetBookDto()
            {
                Id = effect.Id,
                AuthorId = effect.AuthorId,
                Title = effect.Title,
                Genre = effect.Genre,
                Price = effect.Price,
                Quantity = effect.Quantity,
                PublishedYear = effect.PublishedYear,
                CreatedAt = effect.CreatedAt,
                UpdatedAt = effect.UpdatedAt
            };
            if (dto != null)
            {
                return new Response<GetBookDto>(dto);
            }

            return new Response<GetBookDto>(HttpStatusCode.NotFound, "Book Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<GetBookDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}