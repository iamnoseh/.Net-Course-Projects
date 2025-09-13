using System.Net;
using Domain.DTOs.MenuDtos;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuServices(DataContext context):IMenuServices
{
    public async Task<Responce<string>> CreateMenu(CreateMenuDto menu)
    {
        try
        {
            var newMenu = new Menu()
            {
                RestaurantId = menu.RestaurantId,
                Name = menu.Name,
                Description = menu.Description,
                Price = menu.Price,
                Category = menu.Category,
                IsAvailable = menu.IsAvailable,
                PreparationTime = menu.PreparationTime,
                Weight = menu.Weight,
                PhotoUrl = menu.PhotoUrl,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.Menus.AddAsync(newMenu);
            var res = await context.SaveChangesAsync();
            return res > 0
                ?new Responce<string>(HttpStatusCode.Created,"Menu created")
                : new Responce<string>(HttpStatusCode.BadRequest,"Menu not created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> UpdateMenu(UpdateMenuDto menu)
    {
        try
        {
            var updatedMenu = await context.Menus.FirstOrDefaultAsync(x=>x.Id == menu.Id);
            if(updatedMenu == null){return new Responce<string>(HttpStatusCode.NotFound,"Menu not found");}
            updatedMenu.RestaurantId = menu.RestaurantId;
            updatedMenu.Name = menu.Name;
            updatedMenu.Description = menu.Description;
            updatedMenu.Price = menu.Price;
            updatedMenu.Category = menu.Category;
            updatedMenu.IsAvailable = menu.IsAvailable;
            updatedMenu.PreparationTime = menu.PreparationTime;
            updatedMenu.Weight = menu.Weight;
            updatedMenu.PhotoUrl = menu.PhotoUrl;
            updatedMenu.UpdateDate = DateTime.UtcNow;
             var res = await context.SaveChangesAsync();
             return res > 0
                 ?new Responce<string>(HttpStatusCode.OK,"Menu updated")
                 : new Responce<string>(HttpStatusCode.BadRequest,"Menu not updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> DeleteMenu(int Id)
    {
        try
        {
            var deletedMenu = await context.Menus.FirstOrDefaultAsync(x => x.Id == Id);
            if(deletedMenu == null){return new Responce<string>(HttpStatusCode.NotFound,"Menu not found");}
            context.Menus.Remove(deletedMenu);
            var res = await context.SaveChangesAsync();
            return res > 0
                ?new Responce<string>(HttpStatusCode.OK,"Menu deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"Menu not deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<List<GetMenuDto>>> GetMenus()
    {
        try
        {
            var menus = await context.Menus.ToListAsync();
            if (menus.Count == 0)
            {
                return new Responce<List<GetMenuDto>>(HttpStatusCode.NotFound, "Menu not found");
            }

            var dto = menus.Select(x => new GetMenuDto()
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Category = x.Category,
                IsAvailable = x.IsAvailable,
                PreparationTime = x.PreparationTime,
                Weight = x.Weight,
                PhotoUrl = x.PhotoUrl,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetMenuDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetMenuDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<GetMenuDto>> GetMenuById(int Id)
    {
        try
        {
            var menu = await context.Menus.FirstOrDefaultAsync(x => x.Id == Id);
            if(menu == null){return new Responce<GetMenuDto>(HttpStatusCode.NotFound, "Menu not found");}
            var dto = new GetMenuDto()
            {
                Id = menu.Id,
                RestaurantId = menu.RestaurantId,
                Name = menu.Name,
                Description = menu.Description,
                Price = menu.Price,
                Category = menu.Category,
                IsAvailable = menu.IsAvailable,
                PreparationTime = menu.PreparationTime,
                Weight = menu.Weight,
                PhotoUrl = menu.PhotoUrl,
                CreateDate = menu.CreateDate,
                UpdateDate = menu.UpdateDate
            };
            return new Responce<GetMenuDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetMenuDto>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
}