using System.Net;
using Domain.DTOs.OrderDetailDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderDetailServices(DataContext context): IOrderDetailServices
{
    public async Task<Responce<string>> AddOrderDetail(CreateOrderDetailDto orderDetail)
    {
        try
        {
            var newOrderDetail = new OrderDetail()
            {
                OrderId = orderDetail.OrderId,
                MenuItemId = orderDetail.MenuItemId,
                Price = orderDetail.Price,
                Quantity = orderDetail.Quantity,
                SpecialInstructions = orderDetail.SpecialInstructions,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.OrderDetails.AddAsync(newOrderDetail);
            var res = await context.SaveChangesAsync();
            return res > 0 
                ? new Responce<string>(HttpStatusCode.Created,"Order Detail Added")
                : new Responce<string>(HttpStatusCode.InternalServerError,"Order Detail Not Added");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDto orderDetail)
    {
        try
        {
            var update =  await context.OrderDetails.FirstOrDefaultAsync(x=>x.Id == orderDetail.Id);
            if (update == null)
            {
                return new Responce<string>(HttpStatusCode.NotFound, "Order Not Found");
            }
            update.OrderId = orderDetail.OrderId;
            update.MenuItemId = orderDetail.MenuItemId;
            update.Price = orderDetail.Price;
            update.Quantity = orderDetail.Quantity;
            update.SpecialInstructions = orderDetail.SpecialInstructions;
            update.UpdateDate = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Order Detail Updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order Detail Not Updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<string>> DeleteOrderDetail(int Id)
    {
        try
        {
            var delete = await context.OrderDetails.FirstOrDefaultAsync(x => x.Id == Id);
            if (delete == null)
            {
                return new Responce<string>(HttpStatusCode.NotFound, "Order Not Found");
            }
            context.OrderDetails.Remove(delete);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Order Detail Deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"Order Detail Not Deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<List<GetOrderDetailDto>>> GetOrderDetails()
    {
        try
        {
            var orderDetails = await context.OrderDetails.ToListAsync();
            if (orderDetails.Count == 0)
            {
                return new Responce<List<GetOrderDetailDto>>(HttpStatusCode.NotFound, "Order Not Found");
            }
            var dtos = orderDetails.Select(x=> new GetOrderDetailDto()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                MenuItemId = x.MenuItemId,
                Price = x.Price,
                Quantity = x.Quantity,
                SpecialInstructions = x.SpecialInstructions,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderDetailDto>>(dtos);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderDetailDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<GetOrderDetailDto>> GetOrderDetailById(int Id)
    {
        try
        {
            var orderDetail = await context.OrderDetails.FirstOrDefaultAsync(x => x.Id == Id);
            if (orderDetail == null)
            {
                return new Responce<GetOrderDetailDto>(HttpStatusCode.NotFound, "Order Not Found");
            }

            var dto = new GetOrderDetailDto()
            {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                MenuItemId = orderDetail.MenuItemId,
                Price = orderDetail.Price,
                Quantity = orderDetail.Quantity,
                SpecialInstructions = orderDetail.SpecialInstructions,
                CreateDate = orderDetail.CreateDate,
                UpdateDate = orderDetail.UpdateDate
            };
            return new Responce<GetOrderDetailDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetOrderDetailDto>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<List<GetOrderDetailDto>>> GetOrderDetailByOrderId(int OrderId)
    {
        try
        {
            var orderDetails = await context.OrderDetails.Where(x => x.OrderId == OrderId).ToListAsync();
            if (orderDetails.Count == 0)
            {
                return new Responce<List<GetOrderDetailDto>>(HttpStatusCode.NotFound, "Order Details Not Found");
            }
            var dtos = orderDetails.Select(x=> new GetOrderDetailDto()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                MenuItemId = x.MenuItemId,
                Price = x.Price,
                Quantity = x.Quantity,
                SpecialInstructions = x.SpecialInstructions,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderDetailDto>>(dtos);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderDetailDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
}