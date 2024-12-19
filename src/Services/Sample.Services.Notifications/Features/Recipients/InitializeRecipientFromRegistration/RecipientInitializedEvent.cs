using FastEndpoints;

namespace Sample.Services.Notifications.Features.Recipients.InitializeRecipientFromRegistration;

public sealed class RecipientInitializedEvent : IEvent
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Email { get; init; }
    public required string ActivationLink { get; init; }
}