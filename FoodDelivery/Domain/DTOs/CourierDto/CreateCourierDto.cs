using Domain.Enums;

namespace Domain.DTOs.CourierDto;

public class CreateCourierDto
{
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string? CurrentLocation { get; set; }
    public decimal Test { get; set; }
    public TransportType TransportType { get; set; }
}