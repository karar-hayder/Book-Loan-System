using Microsoft.EntityFrameworkCore;

public class LoanRepository : ILoanRepository
{
    private readonly BookLoanSystemDbContext _context;

    public LoanRepository(BookLoanSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
    {
        return await _context.Loans
            .Where(l => l.User_Id == userId)
            .Include(l => l.book)
            .ToListAsync();
    }

    public async Task<Loan?> ReturnLoanAsync(int loanId)
    {
        var loan = await _context.Loans
            .Include(l => l.book)
            .FirstOrDefaultAsync(l => l.Id == loanId);

        if (loan == null)
        {
            return null;
        }

        loan.ReturnDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return loan;
    }
}
