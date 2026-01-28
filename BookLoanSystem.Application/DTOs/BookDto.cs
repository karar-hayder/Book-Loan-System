public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public ICollection<Loan>? Loans { get; set; }
}