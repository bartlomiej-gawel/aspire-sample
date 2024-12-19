using FastEndpoints;

namespace Sample.Services.Notifications.Features.Notifications.GetNotifications;

public sealed class GetNotificationsEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/api/notifications");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}