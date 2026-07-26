using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VetApi.Application.Modules.Identity;
using VetApi.Application.Modules.Identity.RegisterUser;
using VetApi.Domain.Modules.Clinics;
using VetApi.Infrastructure.Persistence;

namespace VetApi.Infrastructure.Identity
{
    public sealed class UserRegistrationService(UserManager<ApplicationUser> userManager, VetDbContext dbContext) : IUserRegistrationService
    {
        public async Task<RegisterUserResult> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            string normalizedEmail = command.Email.Trim();
            var existingUser = await userManager.FindByEmailAsync(normalizedEmail);
            if (existingUser is not null)
            {
                throw new UserRegistrationException(["A user with this email alreay exists."]);
            }
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var userId = Guid.NewGuid();
                var clinicId = Guid.NewGuid();
                var createdAtUtc = DateTime.UtcNow;

                var user = new ApplicationUser
                {
                    Id = userId,
                    UserName = normalizedEmail,
                    Email = normalizedEmail,
                    FirstName = command.FirstName.Trim(),
                    LastName = command.LastName.Trim(),
                    IsActive = true,
                    CreatedAtUtc = createdAtUtc
                };

                var idenityResult = await userManager.CreateAsync(user, command.Password);

                if (!idenityResult.Succeeded)
                {
                    var errors = idenityResult.Errors.Select(errors => errors.Description);
                    throw new UserRegistrationException(errors);
                }

                var clinic = new VeterinaryClinic(
                    clinicId,
                    command.ClinicName.Trim(),
                    command.TimeZoneId.Trim(),
                    createdAtUtc
                );

                var membership = new ClinicMembership(
                    clinicId,
                    userId,
                    ClinicRole.Owner,
                    createdAtUtc
                );

                await dbContext.VeterinaryClinics.AddAsync(clinic, cancellationToken);
                await dbContext.ClinicMemberships.AddAsync(membership, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new RegisterUserResult(userId, clinicId);
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }
    }
}