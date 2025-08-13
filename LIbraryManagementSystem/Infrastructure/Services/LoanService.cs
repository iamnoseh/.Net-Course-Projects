using System.Net;
using Dapper;
using Domain.DTOs.Loan;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class LoanService(DataContext context) : ILoanService
{
    public Response<string> CreateLoan(CreateLoanDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string insert =
                @"insert into loans(bookid, memberid, loandate, returndate, isreturned, createdat, updatedat)
								values (@bookId, @memberId, @loanDate, @returnDate, false, @createdAt, @updatedAt);";
            var effect = connect.Execute(insert, new
            {
                bookId = request.BookId,
                memberId = request.MemberId,
                loanDate = request.LoanDate,
                returnDate = request.ReturnDate,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            });
            const string decrement = @"update books set quantity = quantity - 1 where id = @bookId and quantity > 0;";
            var effect2 = connect.Execute(decrement, new { bookId = request.BookId });

            if (effect > 0 && effect2 > 0)
            {
                return new Response<string>(HttpStatusCode.Created, "Loan successfully created");
            }

            return new Response<string>(HttpStatusCode.BadRequest, "Loan could not be created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> UpdateLoan(UpdateLoanDto request)
    {
        try
        {
            using var connect = context.GetConnection();

            const string update =
                @"update loans set returndate=@returnDate, isreturned=@isReturned, updatedat=@updatedAt where id=@id;";
            var effect = connect.Execute(update, new
            {
                returnDate = request.ReturnDate,
                isReturned = request.IsReturned,
                updatedAt = DateTime.UtcNow,
                id = request.Id
            });

            int bookId = connect.QueryFirst<int>("select bookid from loans where id=@id;", new { id = request.Id });

            int effect2 = 1;
            if (request.IsReturned)
            {
                const string inc = @"update books set quantity = quantity + 1 where id=@bookId;";
                effect2 = connect.Execute(inc, new { bookId });
            }

            if (effect > 0 && effect2 > 0)
            {
                return new Response<string>(HttpStatusCode.OK, "Loan successfully updated");
            }

            return new Response<string>(HttpStatusCode.BadRequest, "Loan could not be updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> DeleteLoan(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"delete from loans where id=@Id;";
            var effect = connect.Execute(query, new { Id = id });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Loan successfully deleted")
                : new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetLoanDto>> GetAllLoans()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from loans;";
            var res = connect.Query<Loan>(query).ToList();
            List<GetLoanDto> dtos = [];
            foreach (var l in res)
            {
                var dto = new GetLoanDto
                {
                    Id = l.Id,
                    BookId = l.BookId,
                    MemberId = l.MemberId,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                    IsReturned = l.IsReturned,
                    CreatedAt = l.CreatedAt,
                    UpdatedAt = l.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetLoanDto>>(dtos)
                : new Response<List<GetLoanDto>>(HttpStatusCode.NotFound, "Loans Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetLoanDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<GetLoanDto> GetLoan(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from loans where id=@Id;";
            var l = connect.QueryFirst<Loan>(query, new { Id = id });
            var dto = new GetLoanDto
            {
                Id = l.Id,
                BookId = l.BookId,
                MemberId = l.MemberId,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate,
                IsReturned = l.IsReturned,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            };
            return new Response<GetLoanDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<GetLoanDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetLoanDto>> GetOverdueLoans()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from loans where isreturned = false and returndate < now();";
            var res = connect.Query<Loan>(query).ToList();
            List<GetLoanDto> dtos = [];
            foreach (var l in res)
            {
                var dto = new GetLoanDto
                {
                    Id = l.Id,
                    BookId = l.BookId,
                    MemberId = l.MemberId,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                    IsReturned = l.IsReturned,
                    CreatedAt = l.CreatedAt,
                    UpdatedAt = l.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetLoanDto>>(dtos)
                : new Response<List<GetLoanDto>>(HttpStatusCode.NotFound, "Loans Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetLoanDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetLoanDto>> GetLoansByMember(int memberId)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from loans where memberid=@memberId;";
            var res = connect.Query<Loan>(query, new { memberId = memberId }).ToList();
            List<GetLoanDto> dtos = [];
            foreach (var l in res)
            {
                var dto = new GetLoanDto
                {
                    Id = l.Id,
                    BookId = l.BookId,
                    MemberId = l.MemberId,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                    IsReturned = l.IsReturned,
                    CreatedAt = l.CreatedAt,
                    UpdatedAt = l.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetLoanDto>>(dtos)
                : new Response<List<GetLoanDto>>(HttpStatusCode.NotFound, "Loans Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetLoanDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}