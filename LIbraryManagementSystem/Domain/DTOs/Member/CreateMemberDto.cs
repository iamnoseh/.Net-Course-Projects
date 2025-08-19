using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Member;

public class CreateMemberDto
{
    [Required]
    [StringLength(50,MinimumLength = 3)]
    public required string FullName { get; set; }
    
    [StringLength(12, MinimumLength = 9)]
    [Phone]
    public string? Phone { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    public DateTime MembershipDate { get; set; }
}


