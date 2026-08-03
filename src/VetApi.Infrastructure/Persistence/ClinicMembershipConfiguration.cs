using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetApi.Infrastructure.Identity;
using VetApi.Domain.Modules.Clinics;

namespace VetApi.Infrastructure.Persistence
{
    public class ClinicMembershipConfiguration : IEntityTypeConfiguration<ClinicMembership>
    {
        public void Configure(
       EntityTypeBuilder<ClinicMembership> builder)
        {
            builder.ToTable("memberships", "clinics");

            builder.HasKey(membership => new
            {
                membership.ClinicId,
                membership.UserId
            });

            builder.Property(membership => membership.Role)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(membership => membership.IsActive)
                .HasDefaultValue(true);

            builder.Property(membership => membership.JoinedAtUtc)
                .HasColumnType("timestamp with time zone");

            builder.HasIndex(membership => membership.UserId);

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(membership => membership.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}