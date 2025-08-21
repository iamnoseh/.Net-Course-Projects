using Domain.DTOs.CouriersDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CourierController(ICourierService service):Controller
{
    [HttpGet]
    public async Task<IActionResult> GetCouriers()
    {
        var res = await service.ListCouriers();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourerById(int id)
    {
        var res = await service.FindCourier(id);
        return Ok(res);
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetCourierOrders(int id)
    {
        var res=await service.GetCourierOrders(id);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourier(CreateCouriersDto courier)
    {
        var res = await service.AddCourier(courier);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourier(UpdateCouriersDto courier)
    {
        var res = await service.UpdateCourier(courier);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourier(int id)
    {
        var res = await service.DeleteCourier(id);
        return Ok(res);
    }
}