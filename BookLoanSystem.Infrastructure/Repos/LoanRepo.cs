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
            .Where(l => l.UserId == userId)
            .Include(l => l.book)
            .ToListAsync();
    }

    public async Task<Loan?> ReturnLoanAsync(int userId, int BookId)
    {
        var loan = await _context.Loans
        .Where(l => l.UserId == userId & l.BookId == BookId)
        .FirstOrDefaultAsync();

        if (loan == null)
        {
            return null;
        }
        loan.ReturnDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return loan;
    }
}
