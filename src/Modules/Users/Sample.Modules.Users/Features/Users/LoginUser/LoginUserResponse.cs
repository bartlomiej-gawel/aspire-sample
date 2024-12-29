namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}