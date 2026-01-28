
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.LoanDate)
            .IsRequired();
        builder.Property(l => l.ReturnDate);

        builder.HasOne<User>()
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Book>()
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.Book_Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
