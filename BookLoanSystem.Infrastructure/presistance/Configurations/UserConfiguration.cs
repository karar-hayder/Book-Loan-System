
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired();

        builder.HasMany(u => u.Loans)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

