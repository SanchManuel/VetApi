using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetApi.Api.Contracts;
using VetApi.Api.Contracts.Auth;
using VetApi.Application.Modules.Identity;
using VetApi.Application.Modules.Identity.RegisterUser;

namespace VetApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRegistrationService registrationService;
        private readonly IValidator<RegisterUserCommand> validator;

        public AuthController(IUserRegistrationService registrationService, IValidator<RegisterUserCommand> validator)
        {
            this.registrationService = registrationService;
            this.validator = validator;
        }

        [HttpPost("register")]
        [ProducesResponseType<RegisterUserResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                registerUserRequest.FirstName,
                registerUserRequest.LastName,
                registerUserRequest.Email,
                registerUserRequest.Password,
                registerUserRequest.ClinicName,
                registerUserRequest.TimeZoneId
            );

            var validationResult = await this.validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                                        .GroupBy(error => error.PropertyName)
                                        .ToDictionary(
                                            group => group.Key,
                                            group => group
                                                .Select(error => error.ErrorMessage)
                                                .ToArray());
                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred."
                };

                return BadRequest(problemDetails);
            }

            try
            {
                var result = await this.registrationService.RegisterAsync(command, cancellationToken);

                var response = new RegisterUserResponse(result.UserId, result.ClinicId);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (UserAlreadyExistsException exception)
            {
                return Conflict(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "User already exists",
                    Detail = exception.Message
                });
            }
            catch (UserRegistrationException exception)
            {
                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "User registration failed"
                };

                problem.Extensions["errors"] = exception.Errors;

                return BadRequest(problem);
            }

        }
    }
}