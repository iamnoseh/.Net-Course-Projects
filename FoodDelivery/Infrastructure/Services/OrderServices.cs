using System.Net;
using Domain.DTOs.OrderDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderServices(DataContext context):IOrderServices
{
    public async Task<Responce<string>> CreateOrder(CreateOrderDto order)
    {
        try
        {
            var newOrder = new Order()
            {
                UserId = order.UserId,
                RestaurantId = order.RestaurantId,
                CourierId = order.CourierId,
                OrderStatus = order.OrderStatus,
                CreatedAt = DateTime.UtcNow,
                DeliveredAt = order.DeliveredAt,
                TotalAmount = order.TotalAmount,
                DeliveryAddress = order.DeliveryAddress,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            context.Orders.Add(newOrder);
            var res= await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Order created")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> UpdateOrder(UpdateOrderDto order)
    {
        try
        {
            var update = await context.Orders.FirstOrDefaultAsync(x=>x.Id == order.Id);
            if(update == null){return new Responce<string>(HttpStatusCode.NotFound,"Order not found");}
            update.UserId = order.UserId;
            update.RestaurantId = order.RestaurantId;
            update.CourierId = order.CourierId;
            update.OrderStatus = order.OrderStatus;
            update.DeliveredAt = order.DeliveredAt;
            update.TotalAmount = order.TotalAmount;
            update.DeliveryAddress = order.DeliveryAddress;
            update.PaymentMethod = order.PaymentMethod;
            update.PaymentStatus = order.PaymentStatus;
            update.UpdateDate = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Order updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> DeleteOrder(int Id)
    {
        try
        {
             var delete = await context.Orders.FirstOrDefaultAsync(x => x.Id == Id);
             if (delete == null)
             {
                 return new Responce<string>(HttpStatusCode.NotFound, "Order not found");
             }
             context.Orders.Remove(delete);
             var res = await context.SaveChangesAsync();
             return res > 0
                 ? new Responce<string>(HttpStatusCode.OK,"Order deleted")
                 : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<List<GetOrderDto>>> GetOrders()
    {
        try
        {
            var orders = await context.Orders.ToListAsync();
            if(orders.Count == 0){return new Responce<List<GetOrderDto>>(HttpStatusCode.NotFound,"Order not found");}
            var dto= orders.Select(x=> new  GetOrderDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                RestaurantId = x.RestaurantId,
                CourierId = x.CourierId,
                OrderStatus = x.OrderStatus,
                CreatedAt = x.CreatedAt,
                DeliveredAt = x.DeliveredAt,
                TotalAmount = x.TotalAmount,
                DeliveryAddress = x.DeliveryAddress,
                PaymentMethod = x.PaymentMethod,
                PaymentStatus = x.PaymentStatus,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<GetOrderDto>> GetOrderById(int Id)
    {
        try
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            if(order == null){return new Responce<GetOrderDto>(HttpStatusCode.NotFound,"Order not found");}

            var dto = new GetOrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                RestaurantId = order.RestaurantId,
                CourierId = order.CourierId,
                OrderStatus = order.OrderStatus,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                TotalAmount = order.TotalAmount,
                DeliveryAddress = order.DeliveryAddress,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                CreateDate = order.CreateDate,
                UpdateDate = order.UpdateDate
            };
            return new Responce<GetOrderDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetOrderDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
} 