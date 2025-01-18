using MediatR;
using Sample.Shared.Contracts.Users;
using Sample.Shared.Infrastructure.Exceptions;

namespace Sample.Modules.Organizations.Features.Organizations.InitializeOrganizationFromRegistration;

internal sealed class InitializeOrganizationFromRegistrationNotificationHandler : INotificationHandler<UserRegisteredNotification>
{
    private readonly ISender _sender;

    public InitializeOrganizationFromRegistrationNotificationHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new InitializeOrganizationFromRegistrationRequest(
                notification.UserId,
                notification.Name,
                notification.Surname,
                notification.Email,
                notification.Phone,
                notification.OrganizationId,
                notification.OrganizationName),
            cancellationToken);

        if (result.IsError)
            throw new CustomException(nameof(InitializeOrganizationFromRegistrationRequest), result.Errors);
    }
}