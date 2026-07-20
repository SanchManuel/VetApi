using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetApi.Infrastructure.Persistence;

namespace VetApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString =
            configuration.GetConnectionString("PostgreSQL")
            ?? throw new InvalidOperationException(
                "PostgresSQL connection string was not confifured."
            );

        services.AddDbContext<VetDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}
