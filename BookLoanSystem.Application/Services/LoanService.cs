
public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICacheService _cacheService;

    public LoanService(ILoanRepository loanRepository, ICacheService cacheService)
    {
        _loanRepository = loanRepository;
        _cacheService = cacheService;
    }

    public async Task<int> CreateLoanAsync(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            User_Id = request.UserId,
            Book_Id = request.BookId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = null
        };

        await _loanRepository.AddAsync(loan);

        string userLoansCacheKey = $"loans_user_{request.UserId}";
        _cacheService.Remove(userLoansCacheKey);

        return loan.Id;
    }

    public async Task<IEnumerable<LoanDto>> GetUserLoansAsync(int userId)
    {
        string userLoansCacheKey = $"loans_user_{userId}";
        var cachedLoans = _cacheService.Get<List<LoanDto>>(userLoansCacheKey);
        if (cachedLoans != null)
        {
            return cachedLoans;
        }

        var loans = await _loanRepository.GetLoansByUserIdAsync(userId);
        var loanDtos = loans
            .Select(l => new LoanDto
            {
                Id = l.Id,
                BookTitle = l.book?.Title,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate
            }).ToList();

        _cacheService.Set(userLoansCacheKey, loanDtos, TimeSpan.FromMinutes(5));
        return loanDtos;
    }

    public async Task<LoanDto?> ReturnLoanAsync(ReturnLoanRequest request)
    {
        var loan = await _loanRepository.ReturnLoanAsync(request.LoanId);
        if (loan != null)
        {
            if (loan.User_Id != 0)
            {
                string userLoansCacheKey = $"loans_user_{loan.User_Id}";
                _cacheService.Remove(userLoansCacheKey);
            }

            return new LoanDto
            {
                Id = loan.Id,
                BookTitle = loan.book?.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };
        }
        return null;
    }
}

