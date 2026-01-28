using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetUserLoans()
    {
        int userId;
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
        {
            return Unauthorized();
        }

        var loans = await _loanService.GetUserLoansAsync(userId);
        return Ok(loans);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateLoan([FromBody] CreateLoanRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        int userId;
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
        {
            return Unauthorized();
        }
        request.UserId = userId;

        var loanId = await _loanService.CreateLoanAsync(request);
        return CreatedAtAction(nameof(GetUserLoans), null, loanId);
    }

    [HttpPost("return")]
    public async Task<ActionResult<LoanDto>> ReturnLoan([FromBody] ReturnLoanRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        int userId;
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
        {
            return Unauthorized();
        }

        var returnedLoan = await _loanService.ReturnLoanAsync(request);

        if (returnedLoan == null)
        {
            return NotFound();
        }

        return Ok(returnedLoan);
    }
}