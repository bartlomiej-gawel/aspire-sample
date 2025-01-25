using ErrorOr;
using MediatR;
using Sample.Modules.Communications.Database;
using Sample.Shared.Contracts.CommunicationsModule;

namespace Sample.Modules.Communications.Features.Recipients.CreateRecipientFromRegistration;

internal sealed class CreateRecipientFromRegistrationRequestHandler : IRequestHandler<CreateRecipientFromRegistrationRequest, ErrorOr<Success>>
{
    private readonly CommunicationsModuleDbContext _dbContext;
    private readonly IPublisher _publisher;

    public CreateRecipientFromRegistrationRequestHandler(
        CommunicationsModuleDbContext dbContext,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
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

        await _publisher.Publish(new RecipientCreatedNotification(
                recipient.Id,
                recipient.Email,
                request.ActivationLink),
            cancellationToken);

        return Result.Success;
    }
}