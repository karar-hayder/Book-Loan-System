using System;
using System.ComponentModel.DataAnnotations;

public enum Role
{
    Admin,
    Customer
}

public class User : IAuditable, ISoftDeletable
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public Role Role { get; set; } = Role.Customer;

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Changes { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
