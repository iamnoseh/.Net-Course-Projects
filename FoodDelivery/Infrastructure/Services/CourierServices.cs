using System.Net;
using AutoMapper;
using Domain.DTOs.CourierDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Infrastructure.Services;
public class CourierServices(DataContext context,IMapper mapper,ILogger<CourierServices> logger) : ICourierServices
{
    public async Task<Responce<string>> AddCourier(CreateCourierDto courier)
    {
        logger.LogInformation("Creating a new courier");
         var map = mapper.Map<Courier>(courier);
         await context.AddAsync(map);
        var res = await context.SaveChangesAsync();
        if (res > 0)
            logger.LogInformation("Courier created successfully");
        else
            logger.LogWarning("Something went wrong");
        return res > 0
            ? new Responce<string>(HttpStatusCode.Created,"Courier created successfully")
            : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Responce<string>> UpdateCourier(UpdateCourierDto courier)
    {
        var map = mapper.Map<Courier>(courier);
        context.Update(map);
        var res = await context.SaveChangesAsync();
        return res > 0
            ? new Responce<string>(HttpStatusCode.OK,"Courier updated successfully")
            : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Responce<string>> DeleteCourier(int id)
    {
        var res = await context.Couriers.FirstOrDefaultAsync(x=>x.Id==id);
        if (res == null)
            return new Responce<string>(HttpStatusCode.NotFound,"Courier not found");
        context.Remove(res);
        var res2 = await context.SaveChangesAsync();
        return res2 > 0
            ? new Responce<string>(HttpStatusCode.OK,"Courier deleted successfully")
            : new Responce<string>(HttpStatusCode.BadRequest,"Something went wrong");
    }

    public async Task<Responce<List<GetCourierDto>>> ListCouriers()
    {
        var couriers =  await context.Couriers.ToListAsync();
        var map = mapper.Map<List<GetCourierDto>>(couriers);
        return new Responce<List<GetCourierDto>>(map);
    }

    public async Task<Responce<GetCourierDto>> FindCourier(int id)
    {
        var courier =  await context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
        var map = mapper.Map<GetCourierDto>(courier);
        return new Responce<GetCourierDto>(map);
    }
}