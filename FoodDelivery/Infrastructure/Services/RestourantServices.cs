using System.Net;
using Domain.DTOs.RestourantDtos;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ResturantServices(DataContext context):IRestourantServices
{
    public async Task<Responce<string>> CreateRestourant(CreateRestourantDto restourant)
    {
        try
        {
            var newRestourant = new Restaurant
            {
                Name = restourant.Name,
                Address = restourant.Address,
                Rating = restourant.Rating,
                WorkingHours = restourant.WorkingHours,
                Description = restourant.Description,
                ContactPhone = restourant.ContactPhone,
                IsActive = restourant.IsActive,
                MinOrderAmount = restourant.MinOrderAmount,
                DeliveryPrice = restourant.DeliveryPrice,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };
            await context.Restourants.AddAsync(newRestourant);
            var res=  await context.SaveChangesAsync();
            return res>0
                ? new Responce<string>(HttpStatusCode.Created,"Restourant Created")
                : new Responce<string>(HttpStatusCode.BadRequest,"Restourant not created");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> UpdateRestourant(UpdateRestourantDto restourant)
    {
        try
        {
            var update = await context.Restourants.FirstOrDefaultAsync(x => x.Id == restourant.Id);
            if (update == null){return new Responce<string>(HttpStatusCode.NotFound,"Restourant not found");}
            update.Name = restourant.Name;
            update.Address = restourant.Address;
            update.Rating = restourant.Rating;
            update.WorkingHours = restourant.WorkingHours;
            update.Description = restourant.Description;
            update.ContactPhone = restourant.ContactPhone;
            update.IsActive = restourant.IsActive;
            update.MinOrderAmount = restourant.MinOrderAmount;
            update.DeliveryPrice = restourant.DeliveryPrice;
            update.UpdateDate = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Restourant Updated")
                : new Responce<string>(HttpStatusCode.BadRequest,"Restourant not updated");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<string>> DeleteRestourant(int Id)
    {
        try
        {
            var delete = await context.Restourants.FirstOrDefaultAsync(x => x.Id == Id);
            if (delete == null) { return new Responce<string>(HttpStatusCode.NotFound,"Restourant not found"); }
            context.Restourants.Remove(delete);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Responce<string>(HttpStatusCode.OK,"Restourant Deleted")
                : new Responce<string>(HttpStatusCode.BadRequest,"Restourant not deleted");
        }
        catch (Exception e)
        {
            return new Responce<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<List<GetRestourantDto>>> GetRestourants()
    {
        try
        {
            var restourants = await context.Restourants.ToListAsync();
            if (restourants.Count == 0) { return new Responce<List<GetRestourantDto>>(HttpStatusCode.NotFound, "Restourant not found"); }

            var restourantDtos = restourants.Select(r => new GetRestourantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Rating = r.Rating,
                WorkingHours = r.WorkingHours,
                Description = r.Description,
                ContactPhone = r.ContactPhone,
                IsActive = r.IsActive,
                MinOrderAmount = r.MinOrderAmount,
                DeliveryPrice = r.DeliveryPrice,
                CreateDate = r.CreateDate,
                UpdateDate = r.UpdateDate
            }).ToList();
            return new Responce<List<GetRestourantDto>>(restourantDtos);
        }
        catch (Exception e)
        {
            return new Responce<List<GetRestourantDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Responce<GetRestourantDto>> GetRestourantById(int Id)
    {
        try
        {
            var restourant = await context.Restourants.FirstOrDefaultAsync(x => x.Id == Id);
            if (restourant == null) { return new Responce<GetRestourantDto>(HttpStatusCode.NotFound, "Restourant not found"); }

            var dto = new GetRestourantDto
            {
                Id = restourant.Id,
                Name = restourant.Name,
                Address = restourant.Address,
                Rating = restourant.Rating,
                WorkingHours = restourant.WorkingHours,
                Description = restourant.Description,
                ContactPhone = restourant.ContactPhone,
                IsActive = restourant.IsActive,
                MinOrderAmount = restourant.MinOrderAmount,
                DeliveryPrice = restourant.DeliveryPrice,
                CreateDate = restourant.CreateDate,
                UpdateDate = restourant.UpdateDate
            };
            return new Responce<GetRestourantDto>(dto);
        }
        catch (Exception e)
        {
            return new Responce<GetRestourantDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}