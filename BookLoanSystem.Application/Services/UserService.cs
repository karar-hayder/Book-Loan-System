
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
            Name = request.Name
        };
        await _userRepository.AddAsync(user);
        return user.Id;
    }
}