

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
        .IsRequired();

        builder.HasMany(b => b.Loans)
        .WithOne()
        .HasForeignKey("BookId")
        .OnDelete(DeleteBehavior.Cascade);
    }
}