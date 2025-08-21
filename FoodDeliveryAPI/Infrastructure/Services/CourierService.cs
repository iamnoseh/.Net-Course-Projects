using System.Net;
using Domain.DTOs.CouriersDto;
using Domain.DTOs.OrderDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
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

    public async Task<Responce<GetCourierOrders>> GetCourierOrders(int id)
    {
        var courier = await context.Couriers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == id);
        var dto = new GetCourierOrders
        {
            CourierId = courier.Id,
            CourierName = courier.FirstName + " " + courier.LastName,
            Orders = courier.Orders.Select(o => new GetOrderDto()
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
            }).ToList(),
            TotalPrice = courier.Orders.Sum(x => x.TotalAmount),
        };
        return new Responce<GetCourierOrders>(dto);
    }
}