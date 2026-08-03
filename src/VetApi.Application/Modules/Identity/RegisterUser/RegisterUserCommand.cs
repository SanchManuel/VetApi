using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Application.Modules.Identity
{
    public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ClinicName,
        string TimeZoneId
    );
}