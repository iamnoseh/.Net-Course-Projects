// using Domain.DTOs.StatusesDto;
// using Infrastructure.Interfaces;
// using Infrastructure.Responces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApp.Controllers;
// [ApiController]
// [Route("api/[controller]")]
// public class StatuseController(IStatusesServices service):ControllerBase
// {
//     [HttpPost]
//     public async Task<Responce<string>> Create(CreateStatusesDto dto)
//     {
//         return await service.AddStatus(dto);
//     }
//
//     [HttpPut]
//     public async Task<Responce<string>> Update(UpdateStatusesDto dto)
//     {
//         return await service.UpdateStatus(dto);
//     }
//
//     [HttpGet]
//     public async Task<Responce<List<GetStatusesDto>>> ListStatuses()
//     {
//         return await service.ListStatuses();
//     }
//
//     [HttpGet("{id}")]
//     public async Task<Responce<GetStatusesDto>> FindStatus(int id)
//     {
//         return await service.FindStatus(id);
//     }
// }