using ErrorOr;
using FluentEmail.Core;
using MediatR;
using Sample.Modules.Notifications.Database;

namespace Sample.Modules.Notifications.Features.Recipients.CreateRecipientFromRegistration;

internal sealed class CreateRecipientFromRegistrationRequestHandler : IRequestHandler<CreateRecipientFromRegistrationRequest, ErrorOr<Success>>
{
    private readonly NotificationsModuleDbContext _dbContext;
    private readonly IFluentEmail _fluentEmail;

    public CreateRecipientFromRegistrationRequestHandler(
        NotificationsModuleDbContext dbContext,
        IFluentEmail fluentEmail)
    {
        _dbContext = dbContext;
        _fluentEmail = fluentEmail;
    }

    public async Task<ErrorOr<Success>> Handle(CreateRecipientFromRegistrationRequest request, CancellationToken cancellationToken)
    {
        var recipient = Recipient.Create(
            request.UserId,
            request.OrganizationId,
            request.OrganizationName,
            request.Name,
            request.Surname,
            request.Email,
            request.Phone);

        await _dbContext.Recipients.AddAsync(recipient, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _fluentEmail
            .To(recipient.Email)
            .Subject("Activate account")
            .Body($"To activate your account, please <a href='{request.ActivationLink}'>click here</a>", isHtml: true)
            .SendAsync();

        return Result.Success;
    }
}