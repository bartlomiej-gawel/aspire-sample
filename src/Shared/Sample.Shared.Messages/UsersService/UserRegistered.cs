namespace Sample.Shared.Messages.UsersService;

public sealed record UserRegistered(
    Guid UserId,
    string UserName,
    string UserSurname,
    string UserEmail,
    string UserPhone,
    Guid OrganizationId,
    string OrganizationName,
    string ActivationLink);