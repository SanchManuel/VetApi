using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetApi.Domain.Modules.Clinics;

namespace VetApi.Infrastructure.Persistence
{
    public sealed class VeterinaryClinicConfiguration : IEntityTypeConfiguration<VeterinaryClinic>
    {
        public void Configure(
        EntityTypeBuilder<VeterinaryClinic> builder)
        {
            builder.ToTable("veterinary_clinics", "clinics");

            builder.HasKey(clinic => clinic.Id);

            builder.Property(clinic => clinic.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(clinic => clinic.TimeZoneId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(clinic => clinic.IsActive)
                .HasDefaultValue(true);

            builder.Property(clinic => clinic.CreatedAtUtc)
                .HasColumnType("timestamp with time zone");

            builder.HasMany(clinic => clinic.Memberships)
                .WithOne(membership => membership.Clinic)
                .HasForeignKey(membership => membership.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}