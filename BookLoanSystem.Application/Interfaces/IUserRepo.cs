public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetUserAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersAsync();
}