using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.CouriersDto;

public class CreateCouriersDto
{
    [Required]
    public required string FirstName{get;set;}
    public string? LastName{get;set;}
    [Phone]
    public required string Phone{get;set;}
    public bool IsAvailable{get;set;}
}