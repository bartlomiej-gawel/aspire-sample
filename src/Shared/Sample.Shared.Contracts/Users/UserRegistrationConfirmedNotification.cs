using MediatR;

namespace Sample.Shared.Contracts.Users;

public sealed record UserRegistrationConfirmedNotification(
    Guid UserId,
    Guid OrganizationId) : INotification;