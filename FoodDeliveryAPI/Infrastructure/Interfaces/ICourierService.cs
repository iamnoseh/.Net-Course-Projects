using Domain.DTOs.CouriersDto;
using Domain.Filter;
using Infrastructure.Responce;
using Infrastructure.Responces;

namespace Infrastructure.Interfaces;

public interface ICourierService
{
    Task<Responce<string>> AddCourier(CreateCouriersDto courier);
    Task<Responce<string>> UpdateCourier(UpdateCouriersDto courier);
    Task<Responce<string>> DeleteCourier(int id);
    Task<Responce<List<GetCouriersDto>>> ListCouriers();
    Task<Responce<GetCouriersDto>> FindCourier(int id);
    Task<Responce<List<GetCourierOrders>>> GetCourierOrders(int id);
    Task<PaginationResponse<List<GetCouriersDto>>> GetCouriersPagination(CourierFilter filter);
}