public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetUserAsync(int userId);
    Task<IEnumerable<User>> GetUsersAsync();
}