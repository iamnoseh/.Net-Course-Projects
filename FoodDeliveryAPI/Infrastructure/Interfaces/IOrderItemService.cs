using Domain.DTOs.OrderItems;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface IOrderItemService
{
    Task<Responce<string>> CreateOrderItem(CreateOrderItemDto dto);
    Task<Responce<string>> UpdateOrderItem(UpdateOrderItemDto dto);
    Task<Responce<string>> DeleteOrderItem(int orderItemId);
    Task<Responce<List<GetOrderItemDto>>> GetAllOrderItems();
    Task<Responce<GetOrderItemDto>> GetOrderItemById(int id);
}