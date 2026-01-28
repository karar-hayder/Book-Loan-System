using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, IJwtTokenService jwtTokenService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { message = "Email and password are required" });
        }

        try
        {

            var user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login attempt failed for email: {Email}", request.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Login attempt with incorrect password for email: {Email}", request.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email, user.Role.ToString());

            _logger.LogInformation("User logged in successfully: {Email}", request.Email);

            return Ok(new LoginDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                ExpiresIn = 3600
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Name))
        {
            return BadRequest(new { message = "Email, password, and name are required" });
        }

        if (request.Password.Length < 6)
        {
            return BadRequest(new { message = "Password must be at least 6 characters long" });
        }

        try
        {
            var existingUser = await _userService.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration attempt with existing email: {Email}", request.Email);
                return BadRequest(new { message = "Email already registered" });
            }

            var passwordHash = HashPassword(request.Password);
            var newUser = new User
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash,
                Role = request.Role
            };

            var userId = await _userService.CreateUserAsync(new CreateUserRequest
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash,
                Role = request.Role
            });

            _logger.LogInformation("User registered successfully: {Email}", request.Email);

            return Created("auth/login", new RegisterDto
            {
                UserId = userId,
                Email = request.Email,
                Name = request.Name,
                Message = "Registration successful. You can now login."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedPassword = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hashedPassword;
        }
    }

    private bool VerifyPassword(string password, string hash)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedPassword = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hashedPassword == hash;
        }
    }
}




