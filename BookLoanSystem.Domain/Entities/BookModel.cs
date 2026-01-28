

using System;
using System.ComponentModel.DataAnnotations;

public class Book : IAuditable, ISoftDeletable
{
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    public string? Changes { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public ICollection<Loan>? Loans { get; set; }
}