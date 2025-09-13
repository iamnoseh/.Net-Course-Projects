using Domain.Enums;

namespace Domain.DTOs.Account;

public class AuthResultDto
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}


