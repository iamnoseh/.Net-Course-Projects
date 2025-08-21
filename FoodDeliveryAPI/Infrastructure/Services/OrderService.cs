using System.Net;
using Domain.DTOs.OrderDto;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(DataContext context) :  IOrderService
{
    public async Task<Responce<string>> AddOrder(CreateOrdersDto order)
    {
        try
        {
            var newOrder = new Order()
            {
                CourierId = order.CourierId,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CreateDate = DateTime.UtcNow,
                OrderDate = DateTime.UtcNow,
            };
            await context.Orders.AddAsync(newOrder);
            var res =  await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Order created successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Order could not be created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
        
    }

    public async Task<Responce<string>> UpdateOrder(UpdateOrdesDto order)
    {
        try
        {
            var oldOrder = await context.Orders.FirstOrDefaultAsync(x=> x.Id == order.Id);
            if (oldOrder == null) return new Responce<string>(HttpStatusCode.NotFound, "Order not found");
            oldOrder.Status = order.Status;
            oldOrder.TotalAmount = order.TotalAmount;
            oldOrder.UpdateDate = DateTime.UtcNow;
            var res =  await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK, "Order updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Order could not be updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<string>> DeleteOrder(int Id)
    {
        try
        {
            var  order = await context.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            if (order == null) return new Responce<string>(HttpStatusCode.NotFound, "Order not found");
            context.Orders.Remove(order);
            var res =  await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK, "Order deleted successfully")
                : new Responce<string>(HttpStatusCode.BadRequest, "Order could not be deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
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