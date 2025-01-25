using ErrorOr;
using FluentEmail.Core;
using MediatR;
using Sample.Modules.Communications.Database;

namespace Sample.Modules.Communications.Features.Messages.SendRegisterConfirmationEmail;

internal sealed class SendRegisterConfirmationEmailRequestHandler : IRequestHandler<SendRegisterConfirmationEmailRequest, ErrorOr<Success>>
{
    private readonly CommunicationsModuleDbContext _dbContext;
    private readonly IFluentEmail _fluentEmail;

    public SendRegisterConfirmationEmailRequestHandler(
        CommunicationsModuleDbContext dbContext,
        IFluentEmail fluentEmail)
    {
        _dbContext = dbContext;
        _fluentEmail = fluentEmail;
    }

    public async Task<ErrorOr<Success>> Handle(SendRegisterConfirmationEmailRequest request, CancellationToken cancellationToken)
    {
        var sendResponse = await _fluentEmail
            .To(request.Email)
            .Subject("Activate account")
            .Body($"To activate your account, please <a href='{request.ActivationLink}'>click here</a>", isHtml: true)
            .SendAsync();

        if (sendResponse is null)
            return MessageErrors.FailedToSendEmail;

        if (!sendResponse.Successful)
            return sendResponse.ErrorMessages.ToErrorOr().Errors;

        var message = Message.Create(
            request.RecipientId,
            "Register confirmation",
            MessageChannel.Email);

        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}