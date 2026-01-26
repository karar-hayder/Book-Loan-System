public interface ILoanRepository
{
    Task AddAsync(Loan loan);
    Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId);
    Task<Loan?> ReturnLoanAsync(int userId, int BookId);
}
