using Domain.DTOs.CourierDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CourierController(ICourierServices services):Controller
{
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCourier(CreateCourierDto dto)
    {
        var res = await services.AddCourier(dto);
        if(res.StatusCode == 201){Log.Information("Courier created successfully");}
        else {Log.Fatal("Error creating courier");}
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCourier(UpdateCourierDto dto)
    {
        var res = await services.UpdateCourier(dto);
        return Ok(res);
    }

    [HttpDelete]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCourier(int id)
    {
        var res = await services.DeleteCourier(id);
        return Ok(res);
    }

    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetCouriers()
    {
        var res = await services.ListCouriers();
        return Ok(res);
    }

    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetCourier(int id)
    {
        var res = await services.FindCourier(id);
        return Ok(res);
    }
}