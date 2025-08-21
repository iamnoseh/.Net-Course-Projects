using System.Net;
using Domain.DTOs.OrderDto;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(DataContext context):IOrderService
{
    public async Task<Responce<string>> AddOrder(CreateOrdersDto order)
    {
        try
        {
            var newOrder = new Order()
            {
                OrderDate = DateTime.UtcNow,
                CourierId = order.CourierId,
                TotalAmount = 0,
                Status = order.Status,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.Orders.AddAsync(newOrder);
            var res= await context.SaveChangesAsync();
            return res > 0
                ?new Responce<string>(HttpStatusCode.Created,"Order successfully created")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<string>> UpdateOrder(UpdateOrdesDto order)
    {
        try
        {
            var oldOrder=await context.Orders.FirstOrDefaultAsync(x=>x.Id==order.Id);
            if(oldOrder==null){return new Responce<string>(HttpStatusCode.NotFound,"Order not found");}
            oldOrder.OrderDate = order.OrderDate;
            oldOrder.CourierId = order.CourierId;
            oldOrder.Status=order.Status;
            oldOrder.UpdateDate = DateTime.UtcNow;
            var res= await context.SaveChangesAsync();
            return res > 0
                ?new Responce<string>(HttpStatusCode.OK,"Order successfully updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be updated");

        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<string>> DeleteOrder(int Id)
    {
        try
        {
            var order=await context.Orders.FirstOrDefaultAsync(x=>x.Id==Id);
            if(order==null){return new Responce<string>(HttpStatusCode.NotFound,"Order not found");}
            context.Orders.Remove(order);
            var res= await context.SaveChangesAsync();
            return res > 0
                ?new Responce<string>(HttpStatusCode.OK,"Order successfully deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order could not be deleted");
        }
        catch (Exception e)
        {
           return new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<List<GetOrderDto>>> ListOrders()
    {
        try
        {
            var res= await context.Orders.Include(order => order.OrderItems).ToListAsync();
            if (res.Count == 0) { return new Responce<List<GetOrderDto>>(HttpStatusCode.NotFound, "Order not found"); }

            var dto = res.Select(o => new GetOrderDto()
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                CourierId = o.CourierId,
                TotalAmount = o.OrderItems.Sum(x=> x.UnitPrice),
                Status = o.Status,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Responce<GetOrderDto>> FindOrder(int Id)
    {
        try
        {
            var res= await context.Orders.Include(order => order.OrderItems).FirstOrDefaultAsync(x=>x.Id==Id);
            if(res==null) { return new Responce<GetOrderDto>(HttpStatusCode.NotFound,"Order not found"); }

            var dto = new GetOrderDto()
            {
                Id = res.Id,
                OrderDate = res.OrderDate,
                CourierId = res.CourierId,
                TotalAmount = res.OrderItems.Sum(x => x.UnitPrice),
                Status = res.Status,
                CreateDate = res.CreateDate,
                UpdateDate = res.UpdateDate
            };
            return new Responce<GetOrderDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetOrderDto>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
    
    public async Task<Responce<List<GetOrderDto>>> FindOrdersByStatus(Status status)
    {
        try
        {
            var  orders= await context.Orders.Where(o => o.Status == status).Include(order => order.OrderItems).ToListAsync();
            if(orders.Count == 0) return new Responce<List<GetOrderDto>>(HttpStatusCode.NotFound,"Order not found");
            var res = orders.Select(o => new GetOrderDto()
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status,
                CourierId = o.CourierId,
                TotalAmount = o.OrderItems.Sum(e => e.UnitPrice),
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderDto>>(res);
        }
        catch 
        {
            return new Responce<List<GetOrderDto>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<List<GetOrderDto>>> ListOrdersByCourier(int courierId)
    {
        try
        {
            var res= await context.Couriers.Include(x=> x.Orders).ThenInclude(x=> x.OrderItems).FirstOrDefaultAsync(x=> x.Id == courierId);
            if(res == null) { return new Responce<List<GetOrderDto>>(HttpStatusCode.NotFound, "Order not found"); }

            var dto = res.Orders.Select(x=> new GetOrderDto()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                CourierId = x.CourierId,
                TotalAmount = x.OrderItems.Sum(e => e.UnitPrice),
                UpdateDate = x.UpdateDate,
                CreateDate = x.CreateDate,
            }).ToList();
            return new Responce<List<GetOrderDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderDto>>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
}