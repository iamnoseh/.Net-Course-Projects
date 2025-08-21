using Domain.DTOs.OrderDto;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responces;

namespace Infrastructure.Services;

public class OrderService(DataContext context) :  IOrderService
{
    public async Task<Responce<string>> AddOrder(CreateOrdersDto order)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<string>> UpdateOrder(UpdateOrdesDto order)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<string>> DeleteOrder(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<GetOrderDto>>> ListOrders()
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<GetOrderDto>> FindOrder(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<GetOrderDto>>> FindOrdersByStatus(Status status)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<GetOrderDto>>> ListOrdersByCourier(int courierId)
    {
        throw new NotImplementedException();
    }
}