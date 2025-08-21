using Domain.DTOs.OrderItems;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderItemsController(IOrderItemService service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrderItems()
    {
        var orderItems = await service.GetAllOrderItems();
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderItem(CreateOrderItemDto dto)
    {
        var orderItem = await service.CreateOrderItem(dto);
        return Ok(orderItem);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto)
    {
        var orderItem = await service.UpdateOrderItem(dto);
        return Ok(orderItem);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderItem(int orderItemId)
    {
        var orderItem = await service.DeleteOrderItem(orderItemId);
        return Ok(orderItem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItemById(int id)
    {
        var orderItem = await service.GetOrderItemById(id);
        return Ok(orderItem);
    }
}