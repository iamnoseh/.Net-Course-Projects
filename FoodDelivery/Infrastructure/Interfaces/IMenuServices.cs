using Domain.DTOs.MenuDtos;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface IMenuServices
{
    Task<Responce<string>> CreateMenu(CreateMenuDto menu);
    Task<Responce<string>> UpdateMenu(UpdateMenuDto menu);
    Task<Responce<string>> DeleteMenu(int Id);
    Task<Responce<List<GetMenuDto>>> GetMenus();
    Task<Responce<GetMenuDto>> GetMenuById(int Id);
}