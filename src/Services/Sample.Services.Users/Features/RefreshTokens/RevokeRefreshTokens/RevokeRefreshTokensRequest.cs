namespace Sample.Services.Users.Features.RefreshTokens.RevokeRefreshTokens;

public sealed class RevokeRefreshTokensRequest
{
    public required Guid UserId { get; init; }
}