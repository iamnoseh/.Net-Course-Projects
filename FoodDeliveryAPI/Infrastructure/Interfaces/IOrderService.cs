using Domain.DTOs.OrderDto;
using Domain.Enums;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<Responce<string>> AddOrder (CreateOrdersDto order);
    Task<Responce<string>> UpdateOrder(UpdateOrdesDto order);
    Task<Responce<string>> DeleteOrder(int Id);
    Task<Responce<List<GetOrderDto>>> ListOrders();
    Task<Responce<GetOrderDto>> FindOrder(int Id);
    Task<Responce<List<GetOrderDto>>> FindOrdersByStatus(Status status);
    Task<Responce<List<GetOrderDto>>> ListOrdersByCourier(int courierId);
}