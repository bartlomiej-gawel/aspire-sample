using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Infrastructure.Jwt;

namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessEndpoint : Endpoint<RefreshUserAccessRequest, ErrorOr<RefreshUserAccessResponse>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly JwtProvider _jwtProvider;

    public RefreshUserAccessEndpoint(
        UsersModuleDbContext dbContext,
        TimeProvider timeProvider,
        JwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
        _jwtProvider = jwtProvider;
    }

    public override void Configure()
    {
        Post("api/users-service/refresh-tokens/refresh-user-access");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<RefreshUserAccessResponse>> ExecuteAsync(RefreshUserAccessRequest req, CancellationToken ct)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;

        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Value == req.RefreshToken, ct);

        if (refreshToken is null)
            return RefreshTokenErrors.NotFound;

        if (refreshToken.ExpireAt < utcDateTime)
            return RefreshTokenErrors.AlreadyExpired;

        var refreshTokenGenerationResult = _jwtProvider.GenerateRefreshToken();
        if (refreshTokenGenerationResult.IsError)
            return refreshTokenGenerationResult.Errors;

        var accessTokenGenerationResult = _jwtProvider.GenerateAccessToken(refreshToken.User);
        if (accessTokenGenerationResult.IsError)
            return accessTokenGenerationResult.Errors;

        refreshToken.Update(
            refreshTokenGenerationResult.Value,
            utcDateTime);

        await _dbContext.SaveChangesAsync(ct);

        var response = new RefreshUserAccessResponse
        {
            AccessToken = refreshTokenGenerationResult.Value,
            RefreshToken = refreshToken.Value
        };

        return response;
    }
}