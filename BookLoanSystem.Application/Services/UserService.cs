
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Role = request.Role
        };
        await _userRepository.AddAsync(user);
        return user.Id;
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user != null)
        {
            var newUser = new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
            return newUser;
        }
        return null;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name
        }).ToList();
    }
}