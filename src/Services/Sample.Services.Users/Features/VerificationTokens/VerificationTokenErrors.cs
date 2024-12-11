using ErrorOr;

namespace Sample.Services.Users.Features.VerificationTokens;

public static class VerificationTokenErrors
{
    public static readonly Error TokenExpired = Error.Conflict(
        "VerificationTokenErrors.TokenExpired",
        "Provided token is expired");
    
    public static readonly Error HttpContextNotAvailable = Error.Unexpected(
        "VerificationTokenErrors.HttpContextNotAvailable",
        "Http context is not available");

    public static readonly Error FailedToGenerate = Error.Conflict(
        "VerificationTokenErrors.FailedToGenerate",
        "Failed to generate verification link.");
}