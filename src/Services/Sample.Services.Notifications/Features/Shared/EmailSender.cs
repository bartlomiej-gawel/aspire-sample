using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Sample.Services.Notifications.Features.Shared;

public sealed class EmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly IConfiguration _configuration;

    public EmailSender(
        ILogger<EmailSender> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(MimeMessage email)
    {
        try
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            _logger.LogInformation("Email sent successfully to {Email}", email.To);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while sending email to {Email}", email.To);
        }
    }
}