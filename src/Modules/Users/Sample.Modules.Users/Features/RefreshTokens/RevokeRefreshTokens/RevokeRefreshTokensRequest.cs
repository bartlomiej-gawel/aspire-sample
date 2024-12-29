namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensRequest
{
    public required Guid UserId { get; init; }
}