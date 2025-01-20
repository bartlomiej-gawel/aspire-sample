using MediatR;
using Sample.Shared.Contracts.Users;
using Sample.Shared.Infrastructure.Exceptions;

namespace Sample.Modules.Communications.Api.Features.Recipients.CreateRecipientFromRegistration;

internal sealed class CreateRecipientFromRegistrationNotificationHandler : INotificationHandler<UserRegisteredNotification>
{
    private readonly ISender _sender;

    public CreateRecipientFromRegistrationNotificationHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CreateRecipientFromRegistrationRequest(
                notification.UserId,
                notification.Name,
                notification.Surname,
                notification.Email,
                notification.Phone,
                notification.OrganizationId,
                notification.OrganizationName,
                notification.ActivationLink),
            cancellationToken);

        if (result.IsError)
            throw new CustomException(nameof(CreateRecipientFromRegistrationRequest), result.Errors);
    }
}