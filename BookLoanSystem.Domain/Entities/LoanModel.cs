using System;

public class Loan : IAuditable
{
    public int Id { get; set; }
    public int User_Id { get; set; }
    public int Book_Id { get; set; }
    public Book? book { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public string? Changes { get; set; }
    public DateTime? UpdatedAt { get; set; }
}