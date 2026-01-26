
public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<int> CreateLoanAsync(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            UserId = request.UserId,
            BookId = request.BookId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = null
        };

        await _loanRepository.AddAsync(loan);
        return loan.Id;
    }

    public async Task<IEnumerable<LoanDto>> GetUserLoansAsync(int userId)
    {
        var loans = await _loanRepository.GetLoansByUserIdAsync(userId);
        return loans
        .Select(l => new LoanDto
        {
            Id = l.Id,
            BookTitle = l.book?.Title,
            LoanDate = l.LoanDate,
            ReturnDate = l.ReturnDate
        }).ToList();
    }
}

