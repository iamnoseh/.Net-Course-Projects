using Domain.DTOs.Loan;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface ILoanService
{
    Response<string> CreateLoan(CreateLoanDto request);
    Response<string> UpdateLoan(UpdateLoanDto request);
    Response<string> DeleteLoan(int id);
    Response<List<GetLoanDto>> GetAllLoans();
    Response<GetLoanDto> GetLoan(int id);
    Response<List<GetLoanDto>> GetOverdueLoans();
    Response<List<GetLoanDto>> GetLoansByMember(int memberId);
}


