using Domain.DTOs.CouriersDto;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CouriersController(ICourierService service):ControllerBase
{
    [HttpPost]
    public async Task<Responce<string>> AddCourier(CreateCouriersDto courier)
    {
        return await service.AddCourier(courier);
    }

    [HttpPut]
    public async Task<Responce<string>> UpdateCourier(UpdateCouriersDto courier)
    {
        return await service.UpdateCourier(courier);
    }

    [HttpDelete]
    public async Task<Responce<string>> DeleteCourier(int id)
    {
        return await service.DeleteCourier(id);
    }

    [HttpGet]
    public async Task<Responce<List<GetCouriersDto>>> GetOrders()
    {
        return await service.ListCouriers();
    }

     [HttpGet("{id}")]
    public async Task<Responce<GetCouriersDto>> GetCourier(int id)
    {
        return await service.FindCourier(id);
    }

    [HttpGet("{id}-orders")]
    public async Task<IActionResult> GetCourierOrders(int id)
    {
        var res = await service.GetCourierOrders(id);
        return Ok(res);
    }
}