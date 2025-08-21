using Domain.DTOs.OrderDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService service):Controller
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var res = await service.ListOrders();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var res = await service.FindOrder(id);
        return Ok(res);
    }

    [HttpGet("{courierId}/orders")]
    public async Task<IActionResult>ListOrdersByCourier(int courierId)
    {
        var res = await service.ListOrdersByCourier(courierId);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrdersDto order)
    {
        var res = await service.AddOrder(order);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder(UpdateOrdesDto order)
    {
        var res = await service.UpdateOrder(order);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var res = await service.DeleteOrder(id);
        return Ok(res);
    }
}