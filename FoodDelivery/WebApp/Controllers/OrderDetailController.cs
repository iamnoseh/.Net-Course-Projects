using Domain.DTOs.OrderDetailDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderDetailController(IOrderDetailServices services):Controller
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailDto dto)
    {
        var res = await services.AddOrderDetail(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailDto dto)
    {
        var res = await services.UpdateOrderDetail(dto);
        return Ok(res);
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var res = await services.DeleteOrderDetail(id);
        return Ok(res);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrderDetails()
    {
        var res = await services.GetOrderDetails();
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetOrderDetail(int id)
    {
        var res = await services.GetOrderDetailById(id);
        return Ok(res);
    }
    
}