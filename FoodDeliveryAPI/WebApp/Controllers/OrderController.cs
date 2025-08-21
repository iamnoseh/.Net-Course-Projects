// using Domain.DTOs.OrderDto;
// using Infrastructure.Interfaces;
// using Infrastructure.Responces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApp.Controllers;
// [ApiController]
// [Route("api/[controller]")]
// public class OrderController(IOrderService service):ControllerBase
// {
//     [HttpPost]
//     public async Task<Responce<string>> AddOrder(CreateOrdersDto order)
//     {
//         return await service.AddOrder(order);
//     }
//
//     [HttpPut]
//     public async Task<Responce<string>> UpdateOrder(UpdateOrdesDto order)
//     {
//         return await service.UpdateOrder(order);
//     }
//
//     [HttpDelete]
//     public async Task<Responce<string>> DeleteOrder(int Id)
//     {
//         return await service.DeleteOrder(Id);
//     }
//
//     [HttpGet]
//     public async Task<Responce<List<GetOrderDto>>> ListOrders()
//     {
//         return await service.ListOrders();
//     }
//
//     [HttpGet("{Id}")]
//     public async Task<Responce<GetOrderDto>> GetOrder(int Id)
//     {
//         return await service.FindOrder(Id);
//     }
//
//     [HttpGet("{customerId}")]
//     public async Task<Responce<List<GetOrderDto>>> GetOrdersByCustomerId(int customerId)
//     {
//         return await service.FindOrdersByCustomer(customerId);
//     }
//
//     [HttpGet("{statusId}")]
//     public async Task<Responce<List<GetOrderDto>>> GetOrdersByStatusId(int statusId)
//     {
//         return await service.FindOrdersByStatus(statusId);
//     }
//
//     [HttpGet("{courierId}")]
//     public async Task<Responce<List<GetOrderDto>>> GetOrdersByCourierId(int courierId)
//     {
//         return await service.ListOrdersByCourier(courierId);
//     }
// }