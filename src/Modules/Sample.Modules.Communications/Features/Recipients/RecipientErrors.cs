using ErrorOr;

namespace Sample.Modules.Communications.Features.Recipients;

internal static class RecipientErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "RecipientErrors.NotFound",
        "Recipient with provided id was not found.");
}