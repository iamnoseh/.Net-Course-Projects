using AutoMapper;
using Domain.Dtos.Book;
using Domain.Dtos.Reader;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<Book, GetBookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        
        CreateMap<Reader, GetReaderDto>();
        CreateMap<CreateReaderDto, Reader>();
        CreateMap<UpdateReaderDto, Reader>();
    }
}