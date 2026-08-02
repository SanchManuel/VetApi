using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Api.Contracts.Auth
{
    public sealed record RegisterUserResponse(
        Guid UserId,
        Guid ClinicId
    );
}