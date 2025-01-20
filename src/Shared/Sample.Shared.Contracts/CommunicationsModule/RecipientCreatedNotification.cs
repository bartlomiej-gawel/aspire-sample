using MediatR;

namespace Sample.Shared.Contracts.CommunicationsModule;

internal sealed record RecipientCreatedNotification() : INotification;