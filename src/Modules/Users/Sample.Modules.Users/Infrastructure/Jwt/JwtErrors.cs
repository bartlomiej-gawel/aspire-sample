using ErrorOr;

namespace Sample.Modules.Users.Infrastructure.Jwt;

internal static class JwtErrors
{
    public static readonly Error AccessTokenGenerationFailed = Error.Unexpected(
        "JwtErrors.AccessTokenGenerationFailed",
        "Failed to generate access token.");

    public static readonly Error RefreshTokenGenerationFailed = Error.Unexpected(
        "JwtErrors.RefreshTokenGenerationFailed",
        "Failed to generate refresh token.");
}