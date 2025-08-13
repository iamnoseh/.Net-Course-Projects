using System.Net;
using Dapper;
using Domain.DTOs.Member;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class MemberService(DataContext context) : IMemberService
{
    public Response<string> CreateMember(CreateMemberDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"Insert into members(fullname,phone,email,membershipdate,createdat,updatedat)
                                   values (@fullName,@phone,@email,@membershipDate,@createdAt,@updatedAt);";
            var effect = connect.Execute(query, new
            {
                fullName = request.FullName,
                phone = request.Phone,
                email = request.Email,
                membershipDate = request.MembershipDate,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.Created, "Member successfully created")
                : new Response<string>(HttpStatusCode.BadRequest, "Member could not be created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> UpdateMember(UpdateMemberDto request)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query =
                @"Update members set fullname=@fullName, phone=@phone, email=@email, membershipdate=@membershipDate, updatedat=@updatedAt where id=@id;";
            var effect = connect.Execute(query, new
            {
                fullName = request.FullName,
                phone = request.Phone,
                email = request.Email,
                membershipDate = request.MembershipDate,
                updatedAt = DateTime.UtcNow,
                id = request.Id
            });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Member successfully updated")
                : new Response<string>(HttpStatusCode.BadRequest, "Member could not be updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<string> DeleteMember(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"Delete from members where id=@Id;";
            var effect = connect.Execute(query, new { Id = id });
            return effect > 0
                ? new Response<string>(HttpStatusCode.OK, "Member successfully deleted")
                : new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<List<GetMemberDto>> GetAllMembers()
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from members;";
            var res = connect.Query<Member>(query).ToList();
            List<GetMemberDto> dtos = [];
            foreach (var m in res)
            {
                var dto = new GetMemberDto
                {
                    Id = m.Id,
                    FullName = m.FullName,
                    Phone = m.Phone,
                    Email = m.Email,
                    MembershipDate = m.MembershipDate,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt
                };
                dtos.Add(dto);
            }

            return dtos.Any()
                ? new Response<List<GetMemberDto>>(dtos)
                : new Response<List<GetMemberDto>>(HttpStatusCode.NotFound, "Members Not Found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<List<GetMemberDto>>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Response<GetMemberDto> GetMember(int id)
    {
        try
        {
            using var connect = context.GetConnection();
            const string query = @"select * from members where id=@id;";
            var m = connect.QueryFirst<Member>(query, new { id });
            var dto = new GetMemberDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Phone = m.Phone,
                Email = m.Email,
                MembershipDate = m.MembershipDate,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            };
            return new Response<GetMemberDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<GetMemberDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}