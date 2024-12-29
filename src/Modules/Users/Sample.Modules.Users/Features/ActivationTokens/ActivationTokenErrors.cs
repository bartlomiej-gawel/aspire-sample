using ErrorOr;

namespace Sample.Modules.Users.Features.ActivationTokens;

internal static class ActivationTokenErrors
{
    public static readonly Error HttpContextNotAvailable = Error.Conflict(
        "ActivationTokenErrors.HttpContextNotAvailable",
        "Cannot create activation link when HttpContext is not available.");

    public static readonly Error LinkGenerationFailed = Error.Conflict(
        "ActivationTokenErrors.LinkGenerationFailed",
        "Failed to generate activation link.");
    
    public static readonly Error NotFound = Error.NotFound(
        "ActivationTokenErrors.NotFound",
        "Provided activation token was not found.");

    public static readonly Error AlreadyExpired = Error.Conflict(
        "ActivationTokenErrors.AlreadyExpired",
        "Provided activation token has already expired.");
}