using Domain.DTOs.MenuDtos;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuControllerer(IMenuServices services):Controller
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateMenu(CreateMenuDto dto)
    {
        var res = await services.CreateMenu(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMenu(UpdateMenuDto dto)
    {
        var res = await services.UpdateMenu(dto);
        return Ok(res);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenu(int id)
    {
        var res = await services.DeleteMenu(id);
        return Ok(res);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMenus()
    {
        var res = await services.GetMenus();
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetMenu(int id)
    {
        var res = await services.GetMenuById(id);
        return Ok(res);
    }
}