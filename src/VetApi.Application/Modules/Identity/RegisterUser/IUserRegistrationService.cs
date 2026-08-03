using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Application.Modules.Identity.RegisterUser
{
    public interface IUserRegistrationService
    {
        Task<RegisterUserResult> RegisterAsync(
            RegisterUserCommand command,
            CancellationToken cancellationToken
        );
    }
}