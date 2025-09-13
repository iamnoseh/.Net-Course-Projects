using Domain.DTOs.CourierDto;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface ICourierServices
{
    Task<Responce<string>> AddCourier(CreateCourierDto courier);
    Task<Responce<string>> UpdateCourier(UpdateCourierDto courier);
    Task<Responce<string>> DeleteCourier(int id);
    Task<Responce<List<GetCourierDto>>> ListCouriers();
    Task<Responce<GetCourierDto>> FindCourier(int id);
}