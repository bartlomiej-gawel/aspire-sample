namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed record RefreshUserAccessResponse(
    string AccessToken,
    string RefreshToken);