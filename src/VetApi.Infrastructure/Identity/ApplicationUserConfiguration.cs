using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApi.Infrastructure.Identity
{
    public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(
        EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(user => user.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(user => user.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(user => user.IsActive)
                .HasDefaultValue(true);

            builder.Property(user => user.CreatedAtUtc)
                .HasColumnType("timestamp with time zone");
        }
    }
}