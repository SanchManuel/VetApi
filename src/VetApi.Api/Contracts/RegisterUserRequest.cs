namespace VetApi.Api.Contracts
{
    public sealed record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ClinicName,
        string TimeZoneId
        );
}