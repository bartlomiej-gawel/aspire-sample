using FastEndpoints;
using MassTransit;
using Sample.Services.Notifications.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Notifications.Features.Recipients.InitializeRecipientFromRegistration;

public sealed class InitializeRecipientFromRegistrationConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<InitializeRecipientFromRegistrationConsumer> _logger;
    private readonly NotificationsServiceDbContext _dbContext;

    public InitializeRecipientFromRegistrationConsumer(
        ILogger<InitializeRecipientFromRegistrationConsumer> logger,
        NotificationsServiceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        try
        {
            _logger.LogInformation("Initializing notifier from registration.");
            
            var recipient = Recipient.Initialize(
                context.Message.UserId,
                context.Message.OrganizationId,
                context.Message.OrganizationName,
                context.Message.UserName,
                context.Message.UserSurname,
                context.Message.UserEmail,
                context.Message.UserPhone);
            
            await _dbContext.Recipients.AddAsync(recipient);
            await _dbContext.SaveChangesAsync();

            await new RecipientInitializedEvent
            {
                
            }.PublishAsync();
            
            _logger.LogInformation("Notifier initialized successfully.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while initializing notifier from registration.");
        }
    }
}