using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Application.Modules.Identity.RegisterUser
{
    public sealed class UserAlreadyExistsException(string email) : Exception($"A user with email {email} aready exits");
}