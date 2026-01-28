public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserRequest request);
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetUsersAsync();
}