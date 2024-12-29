namespace Sample.Modules.Users.Features.Users.RegisterUser;

internal sealed class RegisterUserRequest
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string OrganizationName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; init; }
    public required string RepeatPassword { get; init; }
}