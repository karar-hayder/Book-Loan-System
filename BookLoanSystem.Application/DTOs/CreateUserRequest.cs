using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required]
    [MinLength(3)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    public RoleEnum Role { get; set; }
}