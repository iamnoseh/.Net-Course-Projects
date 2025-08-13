using Domain.DTOs.Author;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IAuthorService
{
    Response<string> CreateAuthor(CreateAuthorDto request);
    Response<string> UpdateAuthor(UpdateAuthorDto request);
    Response<string> DeleteAuthor(int id);
    Response<List<GetAuthorDto>> GetAllAuthors();
    Response<GetAuthorDto> GetAuthor(int id);
}


