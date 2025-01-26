using ErrorOr;

namespace Sample.Modules.Users.Features.RefreshTokens;

internal static class RefreshTokenErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "RefreshTokenErrors.NotFound",
        "Provided refresh token was not found.");

    public static readonly Error AlreadyExpired = Error.Conflict(
        "RefreshTokenErrors.AlreadyExpired",
        "Provided refresh token has already expired.");

    public static readonly Error UnableToGetClaimsValueFromHttpContext = Error.Conflict(
        "RefreshTokenErrors.UnableToGetClaimsValueFromHttpContext",
        "Unable to get claims value from HttpContext.");

    public static readonly Error UnableToParseUserFromHttpContext = Error.Conflict(
        "RefreshTokenErrors.UnableToParseUserFromHttpContext",
        "Unable to parse user from HttpContext.");

    public static readonly Error UnauthorizedUserToRevokeTokens = Error.Unauthorized(
        "RefreshTokenErrors.UnauthorizedUserToRevokeTokens",
        "Unauthorized user to revoke refresh tokens.");
}