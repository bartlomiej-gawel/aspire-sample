namespace Sample.Shared.Messages.UsersService;

public sealed record UserRegistered(
    Guid Id,
    Guid OrganizationId,
    string OrganizationName,
    string Name,
    string Surname,
    string Email,
    string Phone);