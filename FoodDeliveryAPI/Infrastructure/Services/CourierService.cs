using System.Net;
using Domain.DTOs.CouriersDto;
using Domain.DTOs.OrderDto;
using Domain.DTOs.OrderItems;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responce;
using Infrastructure.Responces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourierService(DataContext context) : ICourierService
{
    public async Task<Responce<string>> AddCourier(CreateCouriersDto courier)
    {
        try
        {
            var newCourier = new Courier()
            {
                FirstName = courier.FirstName,
                LastName = courier.LastName,
                Phone = courier.Phone,
                IsAvailable = courier.IsAvailable,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };
            await context.Couriers.AddAsync(newCourier);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Courier created successfully!")
                : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Responce<string>> UpdateCourier(UpdateCouriersDto courier)
    {
        var oldCourier = await context.Couriers.FirstOrDefaultAsync(x=> x.Id == courier.Id);
        if (oldCourier == null) return new Responce<string>(HttpStatusCode.NotFound,"Courier not found");
        oldCourier.FirstName = courier.FirstName;
        oldCourier.LastName = courier.LastName;
        oldCourier.Phone = courier.Phone;
        oldCourier.IsAvailable = courier.IsAvailable;
        oldCourier.UpdateDate = DateTime.UtcNow;
        var res = await context.SaveChangesAsync();
        return new Responce<string>(HttpStatusCode.OK,"Courier updated successfully");
    }

    public async Task<Responce<string>> DeleteCourier(int id)
    {
        var res = await context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null) return new Responce<string>(HttpStatusCode.NotFound,"Courier not found");
        context.Couriers.Remove(res);
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Responce<string>(HttpStatusCode.OK,"Courier deleted successfully")
            : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Responce<List<GetCouriersDto>>> ListCouriers()
    {
        var  res = await context.Couriers.ToListAsync();
        if(res.Count == 0) return new Responce<List<GetCouriersDto>>(HttpStatusCode.NotFound,"Couriers Not Found");
        var dto = res.Select(c => new GetCouriersDto()
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Phone = c.Phone,
            IsAvailable = c.IsAvailable,
            CreateDate = c.CreateDate,
            UpdateDate = c.UpdateDate
        }).ToList();
        return new Responce<List<GetCouriersDto>>(dto);
    }

    public async Task<Responce<GetCouriersDto>> FindCourier(int id)
    {
        var res =  await context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null) return new Responce<GetCouriersDto>(HttpStatusCode.NotFound, "Courier Not Found");
        var dto = new GetCouriersDto()
        {
            Id = res.Id,
            FirstName = res.FirstName,
            LastName = res.LastName,
            Phone = res.Phone,
            IsAvailable = res.IsAvailable,
            CreateDate = res.CreateDate,
            UpdateDate = res.UpdateDate
        };
        return new Responce<GetCouriersDto>(dto);
    }

    public async Task<Responce<List<GetCourierOrders>>> GetCourierOrders(int id)
    {
        try
        {
            var courierItems = context.Couriers.Include(o=>o.Orders).ThenInclude(i=> i.OrderItems).FirstOrDefault(x=> x.Id == id);
            if (courierItems == null) return new Responce<List<GetCourierOrders>>(HttpStatusCode.NotFound, "Items Not Found");
             
            var res = courierItems.Orders.Select(x => new GetCourierOrders()
            {
                CourierId = id,
                CourierName = x.Courier.FirstName + " " + x.Courier.LastName,
                Orders = x.Courier.Orders.Select(e => new GetOrder()
                {
                    OrderId = e.Id,
                    TotalPrice = e.OrderItems.Sum(s => s.UnitPrice),
                    Items = e.OrderItems.Select(i => new GetOrderItemDto()
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        UpdateDate = i.UpdateDate,
                        CreateDate = i.CreateDate
                    }).ToList()
                }).ToList(),
                TotalPrice = x.Courier.Orders.Sum(x=> new {tolat = x.OrderItems.Sum(e=> e.UnitPrice)}.tolat)
            }).ToList();
            return new Responce<List<GetCourierOrders>>(res);
        }
        catch (Exception e)
        {
            return new Responce<List<GetCourierOrders>>(HttpStatusCode.InternalServerError, "Internal server error!");
        }
    }

    public async Task<PaginationResponse<List<GetCouriersDto>>> GetCouriersPagination(CourierFilter filter)
    {
        try
        {
            var query =  context.Couriers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(x=>x.Phone.Contains(filter.PhoneNumber));
            }

            if (filter.IsAvailable.HasValue)
            {
                query = query.Where(x => x.IsAvailable == filter.IsAvailable.Value);
            }
            
            var totalRecords = await query.CountAsync();
            var skip =  (filter.PageNumber - 1) * filter.PageSize;
            var couriers = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
            if(couriers.Count == 0) return new PaginationResponse<List<GetCouriersDto>>(HttpStatusCode.NotFound,"Courier Not Found");
            var dtos = couriers.Select(x => new GetCouriersDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
            }).ToList();
            return new PaginationResponse<List<GetCouriersDto>>(dtos, totalRecords, filter.PageNumber, filter.PageSize);

        }
        catch (Exception e)
        {
            return new PaginationResponse<List<GetCouriersDto>>(HttpStatusCode.InternalServerError,"Internal server error!");
        }
    }
}