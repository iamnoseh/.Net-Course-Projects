using Domain.DTOs.CouriersDto;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface ICourierService
{
    Task<Responce<string>> AddCourier(CreateCouriersDto courier);
    Task<Responce<string>> UpdateCourier(UpdateCouriersDto courier);
    Task<Responce<string>> DeleteCourier(int id);
    Task<Responce<List<GetCouriersDto>>> ListCouriers();
    Task<Responce<GetCouriersDto>> FindCourier(int id);
    Task<Responce<GetCourierOrders>>  GetCourierOrders(int id);
}