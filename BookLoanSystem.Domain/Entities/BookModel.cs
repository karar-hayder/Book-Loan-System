

using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id {get;set;}

    [Required]
    public required string Title {get;set;}
    public ICollection<Loan>? Loans {get;set;}
}