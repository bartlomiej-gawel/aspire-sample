namespace Sample.Shared.Messages.Modules.Users;

public sealed record UserRegistrationConfirmed(
    Guid UserId,
    Guid OrganizationId);