namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserRequest
{
    public required Guid ActivationToken { get; init; }
}