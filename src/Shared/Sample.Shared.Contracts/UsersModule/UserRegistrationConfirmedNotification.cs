using MediatR;

namespace Sample.Shared.Contracts.UsersModule;

public sealed record UserRegistrationConfirmedNotification(
    Guid UserId,
    Guid OrganizationId) : INotification;