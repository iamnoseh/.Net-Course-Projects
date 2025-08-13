using System.Net;
using Dapper;
using Domain.DTOs.Author;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class AuthorService(DataContext context) : IAuthorService
{
    public Response<string> CreateAuthor(CreateAuthorDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"Insert into authors(fullname,birthyear,country,createdat,updatedat)
                                   values (@fullName,@birthYear,@country,@createdAt,@updatedAt);";
            var effect = connect.Execute(query, new
            {
                fullName = request.FullName,
                birthYear = request.BirthYear,
                country = request.Country,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.Created, "Author successfully created")
                : new Response<string>(HttpStatusCode.BadRequest, "Author could not be created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> UpdateAuthor(UpdateAuthorDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query =
                @"Update authors set fullname=@fullName, birthyear=@birthYear, country=@country, updatedat=@updatedAt where id=@id;";
            var effect = connect.Execute(query, new
            {
                fullName = request.FullName,
                birthYear = request.BirthYear,
                country = request.Country,
                updatedAt = DateTime.UtcNow,
                id = request.Id
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Author successfully updated")
                : new Response<string>(HttpStatusCode.BadRequest, "Author could not be updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> DeleteAuthor(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"Delete from authors where id=@Id;";
            var effect = connect.Execute(query, new { Id = id });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Author successfully deleted")
                : new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetAuthorDto>> GetAllAuthors()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from authors;";
            var res = connect.Query<Author>(query).ToList();
            List<GetAuthorDto> dtos = [];
            foreach (var a in res)
            {
                var dto = new GetAuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    BirthYear = a.BirthYear,
                    Country = a.Country,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetAuthorDto>>(dtos)
                : new Response<List<GetAuthorDto>>(HttpStatusCode.NotFound, "Authors Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetAuthorDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<GetAuthorDto> GetAuthor(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from authors where id=@Id;";
            var a = connect.QueryFirst<Author>(query, new { Id = id });
            var dto = new GetAuthorDto
            {
                Id = a.Id,
                FullName = a.FullName,
                BirthYear = a.BirthYear,
                Country = a.Country,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            };
            return new Response<GetAuthorDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<GetAuthorDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}