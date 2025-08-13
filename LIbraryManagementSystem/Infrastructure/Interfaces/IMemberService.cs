using Domain.DTOs.Member;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Response<string> CreateMember(CreateMemberDto request);
    Response<string> UpdateMember(UpdateMemberDto request);
    Response<string> DeleteMember(int id);
    Response<List<GetMemberDto>> GetAllMembers();
    Response<GetMemberDto> GetMember(int id);
}


