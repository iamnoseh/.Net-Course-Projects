using AutoMapper;
using Domain.DTOs.CourierDto;
using Domain.DTOs.MenuDtos;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<Courier, GetCourierDto>().ReverseMap();
        CreateMap<CreateCourierDto, Courier>().ForMember(x=> x.Rating ,
            y=>y.MapFrom(x=>x.Test))
            .ForMember(x => x.CreateDate,
            y => y.MapFrom(x => DateTime.UtcNow)).ReverseMap();
        CreateMap<UpdateCourierDto, Courier>().ReverseMap();

        CreateMap<CreateMenuDto, Menu>().ReverseMap();
        CreateMap<Menu, GetMenuDto>().ReverseMap();
        CreateMap<UpdateMenuDto, Menu>().ReverseMap();
    }
}