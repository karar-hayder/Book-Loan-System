public interface ILoanService
{
    Task<int> CreateLoanAsync(CreateLoanRequest request);
    Task<IEnumerable<LoanDto>> GetUserLoansAsync(int userId);
    Task<LoanDto?> ReturnLoanAsync(int userId, int BookId);
}
