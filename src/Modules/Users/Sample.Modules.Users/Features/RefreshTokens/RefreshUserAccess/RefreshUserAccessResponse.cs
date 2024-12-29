namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}