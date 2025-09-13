using Domain.DTOs.RestourantDtos;
using Infrastructure;

namespace Infrastructure.Interfaces;

public interface IRestourantServices
{
    Task<Responce<string>> CreateRestourant(CreateRestourantDto restourant);
    Task<Responce<string>> UpdateRestourant(UpdateRestourantDto restourant);
    Task<Responce<string>> DeleteRestourant(int Id);
    Task<Responce<List<GetRestourantDto>>> GetRestourants();
    Task<Responce<GetRestourantDto>> GetRestourantById(int Id);
}