using ErrorOr;

namespace Sample.Services.Users.Features.RefreshTokens;

public sealed class RefreshTokenErrors
{
    public static readonly Error UnauthorizedRevoke = Error.Unauthorized(
        "RefreshTokenErrors.UnauthorizedRevoke",
        "You are not authorized to revoke refresh tokens");
    
    public static readonly Error AlreadyExpired = Error.Conflict(
        "RefreshTokenErrors.AlreadyExpired",
        "The refresh token is already expired");
}