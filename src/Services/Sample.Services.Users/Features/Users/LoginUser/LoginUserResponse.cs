namespace Sample.Services.Users.Features.Users.LoginUser;

public sealed class LoginUserResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}