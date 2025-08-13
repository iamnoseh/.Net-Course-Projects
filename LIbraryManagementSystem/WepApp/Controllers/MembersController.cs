using Domain.DTOs.Member;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WepApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMemberService service) : ControllerBase
{
    [HttpGet]
    public Response<List<GetMemberDto>> GetAll() 
        => service.GetAllMembers();

    [HttpGet("{id}")]
    public Response<GetMemberDto> Get(int id) 
        => service.GetMember(id);

    [HttpPost]
    public Response<string> Create(CreateMemberDto request) 
        => service.CreateMember(request);

    [HttpPut]
    public Response<string> Update(UpdateMemberDto request)
        => service.UpdateMember(request);

    [HttpDelete("{id}")]
    public Response<string> Delete(int id) 
        => service.DeleteMember(id);
}


