namespace Sample.Services.Users.Features.Users.LoginUser;

public sealed class LoginUserRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}