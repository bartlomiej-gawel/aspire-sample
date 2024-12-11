namespace Sample.Services.Users.Features.VerificationTokens.VerifyUserRegistration;

public sealed class VerifyUserRegistrationRequest
{
    public required Guid VerificationToken { get; init; }
}