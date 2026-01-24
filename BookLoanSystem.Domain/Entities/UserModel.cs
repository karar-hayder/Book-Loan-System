using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public required string Name { get; set; }

    public ICollection<Loan>? Loans {get;set;}
}
