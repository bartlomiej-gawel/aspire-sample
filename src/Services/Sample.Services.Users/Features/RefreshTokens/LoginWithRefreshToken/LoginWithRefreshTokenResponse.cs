namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}