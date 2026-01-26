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
}
