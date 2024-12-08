namespace Sample.Shared.Messages.UsersService;

public sealed record UserRegistrationConfirmed(
    Guid UserId,
    Guid OrganizationId);