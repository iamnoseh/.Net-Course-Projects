using System.Net;
using Domain.DTOs.CourierDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Services;

public class CourierServices(DataContext context):ICourierServices
{
    public async Task<Responce<string>> AddCourier(CreateCourierDto courier)
    {
        try
        {
            Log.Information("Start adding courier");
            var newCourier = new Courier()
            {
                UserId = courier.UserId,
                Status = courier.Status,
                CurrentLocation = courier.CurrentLocation,
                Rating = courier.Rating,
                TransportType = courier.TransportType,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            await context.Couriers.AddAsync(newCourier);
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Added courier successfully");
            }
            else
            {
                Log.Fatal("Failed to add courier");
            }
            return res > 0
                ? new Responce<string>(HttpStatusCode.Created,"Courier created")
                : new Responce<string>(HttpStatusCode.BadRequest,"Courier not created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> UpdateCourier(UpdateCourierDto courier)
    {
        try
        {
            var update= await context.Couriers.FirstOrDefaultAsync(x=>x.Id == courier.Id);
            if(update== null){return new Responce<string>(HttpStatusCode.NotFound,"Courier not found");}
            update.UserId = courier.UserId;
            update.Status = courier.Status;
            update.CurrentLocation = courier.CurrentLocation;
            update.Rating = courier.Rating;
            update.TransportType = courier.TransportType;
            update.CreateDate = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Courier updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"Courier not updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> DeleteCourier(int id)
    {
        try
        {
            var delete = await context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
            if (delete == null) { return new Responce<string>(HttpStatusCode.NotFound, "Courier not found"); }
            context.Couriers.Remove(delete);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Courier deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"Courier not deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<List<GetCourierDto>>> ListCouriers()
    {
        try
        {
            var couriers = await context.Couriers.ToListAsync();
            if (couriers.Count == 0)
            {
                return new Responce<List<GetCourierDto>>(HttpStatusCode.NotFound,"Courier not found");
            }

            var dto = couriers.Select(x => new GetCourierDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                Status = x.Status,
                CurrentLocation = x.CurrentLocation,
                Rating = x.Rating,
                TransportType = x.TransportType,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            }).ToList();
            return new Responce<List<GetCourierDto>>(dto);
        }
        catch (Exception e)
        {
            return new Responce<List<GetCourierDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Responce<GetCourierDto>> FindCourier(int id)
    {
        try
        {
            var courier = await context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
            if (courier == null)
            {
                return new Responce<GetCourierDto>(HttpStatusCode.NotFound, "Courier not fond");
            }

            var dto = new GetCourierDto()
            {
                Id = courier.Id,
                UserId = courier.UserId,
                Status = courier.Status,
                CurrentLocation = courier.CurrentLocation,
                Rating = courier.Rating,
                TransportType = courier.TransportType,
                CreateDate = courier.CreateDate,
                UpdateDate = courier.UpdateDate
            };
            return new Responce<GetCourierDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetCourierDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}