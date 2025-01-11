using MediatR;

namespace Sample.Shared.Contracts.Users;

public sealed record UserRegisteredNotification(
    Guid UserId,
    string UserName,
    string UserSurname,
    string UserEmail,
    string UserPhone,
    Guid OrganizationId,
    string OrganizationName,
    string ActivationLink) : INotification;