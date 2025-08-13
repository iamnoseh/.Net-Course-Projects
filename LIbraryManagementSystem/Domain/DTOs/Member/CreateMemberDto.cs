namespace Domain.DTOs.Member;

public class CreateMemberDto
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime MembershipDate { get; set; }
}


