namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed record LoginUserResponse(
    string AccessToken,
    string RefreshToken);