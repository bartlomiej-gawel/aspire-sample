using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Shared;

namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenEndpoint : Endpoint<LoginWithRefreshTokenRequest, ErrorOr<LoginWithRefreshTokenResponse>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly TokenProvider _tokenProvider;
    private readonly TimeProvider _timeProvider;

    public LoginWithRefreshTokenEndpoint(
        UsersServiceDbContext dbContext,
        TokenProvider tokenProvider,
        TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
        _timeProvider = timeProvider;
    }

    public override void Configure()
    {
        Post("login");
        Group<RefreshTokenEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task<ErrorOr<LoginWithRefreshTokenResponse>> ExecuteAsync(LoginWithRefreshTokenRequest req, CancellationToken ct)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Value == req.RefreshToken, ct);
        
        if (refreshToken is null || refreshToken.ExpireAt < _timeProvider.GetUtcNow().DateTime)
            return RefreshTokenErrors.AlreadyExpired;
        
        refreshToken.Update(TokenProvider.GenerateRefreshToken());
        
        await _dbContext.SaveChangesAsync(ct);

        return new LoginWithRefreshTokenResponse
        {
            AccessToken = _tokenProvider.GenerateAccessToken(refreshToken.User),
            RefreshToken = refreshToken.Value
        };
    }
}