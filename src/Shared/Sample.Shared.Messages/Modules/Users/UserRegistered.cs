using FastEndpoints;

namespace Sample.Shared.Messages.Modules.Users;

public sealed record UserRegistered(
    Guid UserId,
    string UserName,
    string UserSurname,
    string UserEmail,
    string UserPhone,
    Guid OrganizationId,
    string OrganizationName,
    string ActivationLink) : IEvent;