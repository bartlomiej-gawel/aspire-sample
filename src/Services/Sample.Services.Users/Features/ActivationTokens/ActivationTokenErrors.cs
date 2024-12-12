using ErrorOr;

namespace Sample.Services.Users.Features.ActivationTokens;

public static class ActivationTokenErrors
{
    public static readonly Error TokenExpired = Error.Conflict(
        "ActivationTokenErrors.TokenExpired",
        "Provided token is expired");
    
    public static readonly Error HttpContextNotAvailable = Error.Unexpected(
        "ActivationTokenErrors.HttpContextNotAvailable",
        "Http context is not available");

    public static readonly Error FailedToGenerate = Error.Conflict(
        "ActivationTokenErrors.FailedToGenerate",
        "Failed to generate activation link.");
}