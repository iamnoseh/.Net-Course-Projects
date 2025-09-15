using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.AuthDto;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    [Required]
    public required string Address { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
    
    public UserRole Role { get; set; } = UserRole.User;
}
