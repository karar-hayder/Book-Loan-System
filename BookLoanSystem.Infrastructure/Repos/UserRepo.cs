public class UserRepository : IUserRepository
{
    private readonly BookLoanSystemDbContext _context;

    public UserRepository(BookLoanSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}