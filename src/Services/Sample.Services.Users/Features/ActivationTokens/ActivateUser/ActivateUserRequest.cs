namespace Sample.Services.Users.Features.ActivationTokens.ActivateUser;

public sealed class ActivateUserRequest
{
    public required Guid ActivationToken { get; init; }
}