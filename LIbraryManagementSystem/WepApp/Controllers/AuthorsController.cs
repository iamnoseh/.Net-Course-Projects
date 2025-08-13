using Domain.DTOs.Author;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WepApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService service) : ControllerBase
{
    [HttpGet]
    public Response<List<GetAuthorDto>> GetAll() 
        => service.GetAllAuthors();

    [HttpGet("{id}")]
    public Response<GetAuthorDto> Get(int id) 
        => service.GetAuthor(id);

    [HttpPost]
    public Response<string> Create(CreateAuthorDto request) 
        => service.CreateAuthor(request);

    [HttpPut]
    public Response<string> Update( UpdateAuthorDto request)
        => service.UpdateAuthor(request);
    

    [HttpDelete("{id}")]
    public Response<string> Delete(int id) 
        => service.DeleteAuthor(id);
}


