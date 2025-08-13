namespace Domain.DTOs.Member;

public class GetMemberDto : UpdateMemberDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


