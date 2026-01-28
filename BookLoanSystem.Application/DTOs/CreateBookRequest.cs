using System.ComponentModel.DataAnnotations;

public class CreateBookRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(512)]
    public required string Title { get; set; }
}