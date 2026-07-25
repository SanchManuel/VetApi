using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Application.Modules.Identity.RegisterUser
{
    public sealed record RegisterUserResult(
        Guid UserId,
        Guid ClinicId
    );
}