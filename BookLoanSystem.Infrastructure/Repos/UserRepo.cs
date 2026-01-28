using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> GetUserAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}