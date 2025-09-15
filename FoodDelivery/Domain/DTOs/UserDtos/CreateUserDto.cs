using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.UserDtos;

public class CreateUserDto
{
    [Required]
    [StringLength(50,MinimumLength = 3)]
    public required string Name { get; set; }
    public required string Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [StringLength(200, MinimumLength = 6)]
    public required string Password { get; set; }
    public required string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
}