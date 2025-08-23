using System.Net;
using Domain.DTOs.OrderItems;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responce;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderItemService(DataContext context):  IOrderItemService
{
    public async Task<Responce<string>> CreateOrderItem(CreateOrderItemDto dto)
    {
        try
        {
            var newItem = new OrderItem()
            {
                OrderId = dto.OrderId,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity,
                ProductId = dto.ProductId,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.OrderItems.AddAsync(newItem);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Order item created successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");

        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<string>> UpdateOrderItem(UpdateOrderItemDto dto)
    {
        try
        {
            var oldItem = await context.OrderItems.FirstOrDefaultAsync(x=> x.OrderId == dto.OrderId);
            if(oldItem == null) return new Responce<string>(HttpStatusCode.NotFound,"Order item not found");

            oldItem.UnitPrice = dto.UnitPrice;
            oldItem.Quantity = dto.Quantity;
            oldItem.ProductId = dto.ProductId;
            oldItem.UpdateDate = DateTime.UtcNow;
            oldItem.OrderId = dto.OrderId;
            var res = await context.SaveChangesAsync();
            return res > 0 
                ? new Responce<string>(HttpStatusCode.OK,"Order item updated successfully")
                : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<string>> DeleteOrderItem(int orderItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<GetOrderItemDto>>> GetAllOrderItems()
    {
        try
        {
            var items =  await context.OrderItems.ToListAsync();
            if(items.Count == 0) return new Responce<List<GetOrderItemDto>>(HttpStatusCode.NotFound,"Order item not found");
            var res = items.Select(i=> new GetOrderItemDto()
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                CreateDate = i.CreateDate,
                UpdateDate = i.UpdateDate
            }).ToList();
            return new Responce<List<GetOrderItemDto>>(res);
        }
        catch (Exception e)
        {
            return new Responce<List<GetOrderItemDto>>(HttpStatusCode.InternalServerError,"Internal server error");
        }
    }

    public async Task<Responce<GetOrderItemDto>> GetOrderItemById(int id)
    {
        try
        {
            var  res = await context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
            if(res == null) return new Responce<GetOrderItemDto>(HttpStatusCode.NotFound,"Order item not found");
            var dto = new GetOrderItemDto()
            {
                Id = res.Id,
                OrderId = res.OrderId,
                ProductId = res.ProductId,
                Quantity = res.Quantity,
                UnitPrice = res.UnitPrice,
                UpdateDate = res.UpdateDate,
                CreateDate = res.CreateDate,
            };
            return new Responce<GetOrderItemDto>(dto);

        }
        catch (Exception e)
        {
            return  new Responce<GetOrderItemDto>(HttpStatusCode.InternalServerError,"Internal server error");      
        }
    }
}