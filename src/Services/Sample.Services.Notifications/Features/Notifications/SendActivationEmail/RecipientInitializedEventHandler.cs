using FastEndpoints;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Sample.Services.Notifications.Features.Recipients.InitializeRecipientFromRegistration;

namespace Sample.Services.Notifications.Features.Notifications.SendActivationEmail;

public sealed class RecipientInitializedEventHandler : IEventHandler<RecipientInitializedEvent>
{
    public async Task HandleAsync(RecipientInitializedEvent eventModel, CancellationToken ct)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(""));
        email.To.Add(MailboxAddress.Parse(""));
        email.Subject = "Activate account for Aspire Sample";
        email.Body = new TextPart(TextFormat.Html);

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls, ct);
        await smtp.AuthenticateAsync("", "", ct);
        await smtp.SendAsync(email, ct);
        await smtp.DisconnectAsync(true, ct);
        
        throw new NotImplementedException();
    }
}