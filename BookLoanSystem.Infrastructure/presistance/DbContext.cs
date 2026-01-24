using Microsoft.EntityFrameworkCore;

public class BookLoanSystemDbContext : DbContext
{
    public BookLoanSystemDbContext(DbContextOptions<BookLoanSystemDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new LoanConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
