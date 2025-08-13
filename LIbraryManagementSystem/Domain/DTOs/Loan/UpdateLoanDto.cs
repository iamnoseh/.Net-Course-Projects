namespace Domain.DTOs.Loan;

public class UpdateLoanDto : CreateLoanDto
{
    public int Id { get; set; }
    public bool IsReturned { get; set; }
}


