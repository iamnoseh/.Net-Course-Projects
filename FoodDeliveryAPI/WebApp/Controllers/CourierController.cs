// using Domain.DTOs.CouriersDto;
// using Infrastructure.Interfaces;
// using Infrastructure.Responces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApp.Controllers;
// [ApiController]
// [Route("api/[controller]")]
// public class CourierController(ICourierServices services):ControllerBase
// {
//     [HttpPost]
//     public async Task<Responce<string>> AddCourier(CreateCouriersDto courier)
//     {
//         return await services.AddCourier(courier);
//     }
//
//     [HttpPut]
//     public async Task<Responce<string>> UpdateCourier(UpdateCouriersDto courier)
//     {
//         return await services.UpdateCourier(courier);
//     }
//
//     [HttpDelete]
//     public async Task<Responce<string>> DeleteCourier(int id)
//     {
//         return await services.DeleteCourier(id);
//     }
//
//     [HttpGet]
//     public async Task<Responce<List<GetCouriersDto>>> GetOrders()
//     {
//         return await services.ListCouriers();
//     }
//
//      [HttpGet("{id}")]
//     public async Task<Responce<GetCouriersDto>> GetCourier(int id)
//     {
//         return await services.FindCourier(id);
//     }
// }