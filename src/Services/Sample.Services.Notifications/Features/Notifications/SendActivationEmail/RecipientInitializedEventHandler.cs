using FastEndpoints;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Sample.Services.Notifications.Features.Recipients.InitializeRecipientFromRegistration;
using Sample.Services.Notifications.Features.Shared;

namespace Sample.Services.Notifications.Features.Notifications.SendActivationEmail;

public sealed class RecipientInitializedEventHandler : IEventHandler<RecipientInitializedEvent>
{
    private readonly EmailSender _emailSender;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RecipientInitializedEventHandler> _logger;

    public RecipientInitializedEventHandler(
        EmailSender emailSender,
        IConfiguration configuration,
        ILogger<RecipientInitializedEventHandler> logger)
    {
        _emailSender = emailSender;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task HandleAsync(RecipientInitializedEvent eventModel, CancellationToken ct)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Email:Username"]));
            email.To.Add(MailboxAddress.Parse(eventModel.Email));
            email.Subject = "Activate account for Aspire Sample";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"""
                        <p>Dear {eventModel.Name} {eventModel.Surname},</p>
                        <p>Thank you for registering with Aspire Sample. Please activate your account by clicking the link below:</p>
                        <p><a href="{eventModel.ActivationLink}">Activate Account</a></p>
                        <p>Best regards,<br>The Aspire Sample Team</p>
                        """
            };
            
            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to send activation email to {Email}", eventModel.Email);
        }
    }
}