using Domain.DTOs.OrderDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderControllerr(IOrderServices service):Controller
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var res = await service.CreateOrder(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
    {
        var res = await service.UpdateOrder(dto);
        return Ok(res);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var res = await service.DeleteOrder(id);
        return Ok(res);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrders()
    {
        var res = await service.GetOrders();
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetOrder(int id)
    {
        var res = await service.GetOrderById(id);
        return Ok(res);
    }
}


