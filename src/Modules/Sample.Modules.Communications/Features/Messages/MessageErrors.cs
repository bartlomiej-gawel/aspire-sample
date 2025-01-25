using ErrorOr;

namespace Sample.Modules.Communications.Features.Messages;

internal static class MessageErrors
{
    public static readonly Error FailedToSendEmail = Error.NotFound(
        "MessageErrors.FailedToSendEmail",
        "Failed to send email.");
}