using Domain.DTOs.RestourantDtos;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestourantController(IRestourantServices services):Controller
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRestourant(CreateRestourantDto dto)
    {
        var res = await services.CreateRestourant(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRestourant(UpdateRestourantDto dto)
    {
        var res = await services.UpdateRestourant(dto);
        return Ok(res);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRestourant(int id)
    {
        var res = await services.DeleteRestourant(id);
        return Ok(res);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRestourants()
    {
        var res = await services.GetRestourants();
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetRestourant(int id)
    {
        var res = await services.GetRestourantById(id);
        return Ok(res);
    }
}