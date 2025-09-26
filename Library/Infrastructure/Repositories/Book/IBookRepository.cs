using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Book;

public interface IBookRepository
{
    Task<Response<List<Domain.Entities.Book>>> GetAllAsync();
    Task<Response<Domain.Entities.Book?>> GetByIdAsync(int id);
    Task<Response<Domain.Entities.Book>> CreateAsync(Domain.Entities.Book book);
    Task<Response<Domain.Entities.Book>> UpdateAsync(Domain.Entities.Book book);
    Task<Response<bool>> DeleteAsync(int id);
}