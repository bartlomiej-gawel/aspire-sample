using MediatR;

namespace Sample.Shared.Contracts.Users;

public sealed record UserRegisteredNotification(
    Guid UserId,
    string Name,
    string Surname,
    string Email,
    string Phone,
    Guid OrganizationId,
    string OrganizationName,
    string ActivationLink) : INotification;