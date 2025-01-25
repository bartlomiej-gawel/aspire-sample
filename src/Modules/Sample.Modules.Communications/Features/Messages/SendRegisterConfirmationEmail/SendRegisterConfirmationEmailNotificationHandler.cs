using MediatR;
using Sample.Shared.Contracts.CommunicationsModule;
using Sample.Shared.Infrastructure.Exceptions;

namespace Sample.Modules.Communications.Features.Messages.SendRegisterConfirmationEmail;

internal sealed class SendRegisterConfirmationEmailNotificationHandler : INotificationHandler<RecipientCreatedNotification>
{
    private readonly ISender _sender;

    public SendRegisterConfirmationEmailNotificationHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(RecipientCreatedNotification notification, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SendRegisterConfirmationEmailRequest(
                notification.RecipientId,
                notification.Email,
                notification.ActivationLink),
            cancellationToken);

        if (result.IsError)
            throw new CustomException(nameof(SendRegisterConfirmationEmailRequest), result.Errors);
    }
}