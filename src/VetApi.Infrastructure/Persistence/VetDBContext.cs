using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetApi.Infrastructure.Identity;
using VetApi.Domain.Modules.Clinics;
namespace VetApi.Infrastructure.Persistence;

public sealed class VetDbContext(
    DbContextOptions<VetDbContext> options)
    : IdentityDbContext<
        ApplicationUser,
        IdentityRole<Guid>,
        Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureIdentityTables(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VetDbContext).Assembly);
    }

    private static void ConfigureIdentityTables(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("users", "identity");

        modelBuilder.Entity<IdentityRole<Guid>>()
            .ToTable("roles", "identity");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles", "identity");

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims", "identity");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins", "identity");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims", "identity");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens", "identity");
    }
    public DbSet<VeterinaryClinic> VeterinaryClinics =>
    Set<VeterinaryClinic>();

    public DbSet<ClinicMembership> ClinicMemberships =>
        Set<ClinicMembership>();
}