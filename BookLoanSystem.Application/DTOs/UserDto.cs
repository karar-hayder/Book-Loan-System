public class UserDto
{
    public int Id {get;set;}
    public required string Name { get; set; }
    public ICollection<Loan>? Loans {get;set;}
}