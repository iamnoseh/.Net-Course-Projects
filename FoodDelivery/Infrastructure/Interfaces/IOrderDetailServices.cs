using Domain.DTOs.OrderDetailDto;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface IOrderDetailServices
{
    Task<Responce<string>> AddOrderDetail(CreateOrderDetailDto orderDetail);
    Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDto orderDetail);
    Task<Responce<string>> DeleteOrderDetail(int Id);
    Task<Responce<List<GetOrderDetailDto>>> GetOrderDetails();
    Task<Responce<GetOrderDetailDto>> GetOrderDetailById(int Id);
    Task<Responce<List<GetOrderDetailDto>>> GetOrderDetailByOrderId(int OrderId);
}