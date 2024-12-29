namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}