using Domain.DTOs.OrderDto;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface IOrderServices
{
    Task<Responce<string>> CreateOrder(CreateOrderDto order);
    Task<Responce<string>> UpdateOrder(UpdateOrderDto order);
    Task<Responce<string>> DeleteOrder(int Id);
    Task<Responce<List<GetOrderDto>>> GetOrders();
    Task<Responce<GetOrderDto>> GetOrderById(int Id);
}