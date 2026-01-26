public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserRequest request);
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserDto>> GetUsersAsync();
}