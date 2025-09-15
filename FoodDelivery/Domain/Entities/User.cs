using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [Required]
    public required string Address { get; set; }

    public DateTime RegistrationDate { get; set; }
    
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public Courier? Courier { get; set; }
    public List<Order>? Orders { get; set; }
}