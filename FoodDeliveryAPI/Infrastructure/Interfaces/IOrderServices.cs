using Domain.DTOs.OrderDto;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface IOrderServices
{
    Task<Responce<string>> AddOrder (CreateOrdersDto order);
    Task<Responce<string>> UpdateOrder(UpdateOrdesDto order);
    Task<Responce<string>> DeleteOrder(int Id);
    Task<Responce<List<GetOrderDto>>> ListOrders();
    Task<Responce<GetOrderDto>> FindOrder(int Id);
    Task<Responce<List<GetOrderDto>>> FindOrdersByCustomer(int customerId);
    Task<Responce<List<GetOrderDto>>> FindOrdersByStatus(int statusId);
    Task<Responce<List<GetOrderDto>>> ListOrdersByCourier(int courierId);
}