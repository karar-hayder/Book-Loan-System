public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserRequest request);

}