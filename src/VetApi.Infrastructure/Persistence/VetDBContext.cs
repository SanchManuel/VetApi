using Microsoft.EntityFrameworkCore;

namespace VetApi.Infrastructure.Persistence;

public sealed class VetDbContext(DbContextOptions<VetDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VetDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
