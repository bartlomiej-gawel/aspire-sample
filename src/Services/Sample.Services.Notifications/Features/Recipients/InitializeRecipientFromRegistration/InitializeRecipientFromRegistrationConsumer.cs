using FluentEmail.Core;
using MassTransit;
using Sample.Services.Notifications.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Notifications.Features.Recipients.InitializeRecipientFromRegistration;

public sealed class InitializeRecipientFromRegistrationConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<InitializeRecipientFromRegistrationConsumer> _logger;
    private readonly NotificationsServiceDbContext _dbContext;
    private readonly IFluentEmail _fluentEmail;

    public InitializeRecipientFromRegistrationConsumer(
        ILogger<InitializeRecipientFromRegistrationConsumer> logger,
        NotificationsServiceDbContext dbContext,
        IFluentEmail fluentEmail)
    {
        _logger = logger;
        _dbContext = dbContext;
        _fluentEmail = fluentEmail;
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
            
            _logger.LogInformation("Notifier initialized successfully.");
            
            var emailResult = await _fluentEmail
                .To(recipient.Email)
                .Subject("Activate account for aspire sample")
                .Body($"To activate your account <a href='{context.Message.ActivationLink}'>click here</a>", isHtml: true)
                .SendAsync();

            if (emailResult.Successful)
                _logger.LogInformation("Email sent successfully.");
            else
                _logger.LogError("Email sending failed.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while initializing notifier from registration.");
        }
    }
}