using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required]
    [MinLength(3)]
    public required string Name {get;set;}
}