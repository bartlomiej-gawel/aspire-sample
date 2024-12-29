namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessRequest
{
    public required string RefreshToken { get; init; }
}