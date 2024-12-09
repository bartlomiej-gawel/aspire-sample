namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenRequest
{
    public required string RefreshToken { get; init; }
}