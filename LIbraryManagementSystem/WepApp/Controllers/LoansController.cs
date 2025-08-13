
using Domain.DTOs.Loan;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WepApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController(ILoanService service) : ControllerBase
{
    [HttpGet]
    public Response<List<GetLoanDto>> GetAll() 
        => service.GetAllLoans();

    [HttpGet("{id}")]
    public Response<GetLoanDto> Get(int id) 
        => service.GetLoan(id);

    [HttpPost]
    public Response<string> Create(CreateLoanDto request) 
        => service.CreateLoan(request);

    [HttpPut]
    public Response<string> Update( UpdateLoanDto request)
        => service.UpdateLoan(request);
    

    [HttpDelete("{id}")]
    public Response<string> Delete(int id) 
        => service.DeleteLoan(id);

    [HttpGet("overdue")]
    public Response<List<GetLoanDto>> Overdue() 
        => service.GetOverdueLoans();

    [HttpGet("member/{memberId}")]
    public Response<List<GetLoanDto>> MemberLoans(int memberId) 
        => service.GetLoansByMember(memberId);
}


