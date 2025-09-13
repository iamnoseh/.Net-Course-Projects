using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; set; }
    public required string Email { get; set; }
    [Phone] public string? Phone { get; set; }
    [StringLength(1000,MinimumLength = 6)] 
    public required string Password { get; set; }
    public required string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }

    public Courier? Courier { get; set; }
    public List<Order>? Orders { get; set; }
}