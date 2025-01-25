using MediatR;

namespace Sample.Shared.Contracts.CommunicationsModule;

public sealed record RecipientCreatedNotification(
    Guid RecipientId,
    string Email,
    string ActivationLink) : INotification;