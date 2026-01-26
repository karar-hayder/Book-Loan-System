using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    // GET /loans/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetUserLoans(int userId)
    {
        var loans = await _loanService.GetUserLoansAsync(userId);
        return Ok(loans);
    }

    // POST /loans
    [HttpPost]
    public async Task<ActionResult<int>> CreateLoan([FromBody] CreateLoanRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var loanId = await _loanService.CreateLoanAsync(request);
        return CreatedAtAction(nameof(GetUserLoans), new { userId = request.UserId }, loanId);
    }

    [HttpPost("return")]
    public async Task<ActionResult<LoanDto>> ReturnLoan([FromBody] ReturnLoanRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var returnedLoan = await _loanService.ReturnLoanAsync(request.UserId, request.BookId);

        if (returnedLoan == null)
        {
            return NotFound();
        }

        return Ok(returnedLoan);
    }
}