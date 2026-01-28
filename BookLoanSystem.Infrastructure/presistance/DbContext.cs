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

        // Add global query filters for soft deleted entities
        modelBuilder.Entity<Book>()
            .HasQueryFilter(b => b.IsDeleted == false || b.IsDeleted == null);

        modelBuilder.Entity<User>()
            .HasQueryFilter(u => u.IsDeleted == false || u.IsDeleted == null);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditable && e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var auditable = (IAuditable)entry.Entity;
            auditable.UpdatedAt = DateTime.UtcNow;
            auditable.Changes = $"Updated at {DateTime.UtcNow:O}";
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditable && e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var auditable = (IAuditable)entry.Entity;
            auditable.UpdatedAt = DateTime.UtcNow;
            auditable.Changes = $"Updated at {DateTime.UtcNow:O}";
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
