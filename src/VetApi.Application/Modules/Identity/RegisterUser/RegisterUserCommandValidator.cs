using System.Data;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using FluentValidation;

namespace VetApi.Application.Modules.Identity.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(Command => Command.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(command => command.LastName).NotEmpty().MaximumLength(100);
            RuleFor(command => command.Email).NotEmpty().MinimumLength(8).Matches("[A-Z]")
            .WithMessage("Password must  contain an uppercase latter")
            .Matches("[a-z]").WithMessage("Password m ust contaon a lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain a number");
            RuleFor(command => command.ClinicName).NotEmpty().MaximumLength(150);
            RuleFor(command => command.TimeZoneId).NotEmpty().MaximumLength(100)
            .Must(BeValidTimeZone).WithMessage("The specified time zone is invalid");
        }

        public static bool BeValidTimeZone(string TimeZoneId)
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                return true;
            }
            catch (TimeZoneNotFoundException)
            {
                return false;
            }
        }
    }
}