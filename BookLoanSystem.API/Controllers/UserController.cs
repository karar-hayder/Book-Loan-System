using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost()]
    public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserRequest request)
    {
        await _userService.CreateUserAsync(request);
        return Created();
    }
    [HttpGet("userId:int")]
    public async Task<ActionResult<UserDto>> GetUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

}