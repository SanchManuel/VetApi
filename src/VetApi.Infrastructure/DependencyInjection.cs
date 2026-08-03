using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using VetApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using VetApi.Infrastructure.Identity;
using VetApi.Application.Modules.Identity.RegisterUser;

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

        services
            .AddHealthChecks()
            .AddDbContextCheck<VetDbContext>(
                name: "postgresql",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["database", "ready"]
            );

        services
        .AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(15);
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<VetDbContext>();

        services.AddScoped<IUserRegistrationService, UserRegistrationService>();

        return services;
    }
}
