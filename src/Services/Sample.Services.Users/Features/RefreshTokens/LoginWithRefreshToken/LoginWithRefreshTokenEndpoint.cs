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

    public LoginWithRefreshTokenEndpoint(
        UsersServiceDbContext dbContext,
        TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public override void Configure()
    {
        Post("api/users-service/refresh-tokens/login");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<LoginWithRefreshTokenResponse>> ExecuteAsync(LoginWithRefreshTokenRequest req, CancellationToken ct)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Value == req.RefreshToken, ct);
        
        if (refreshToken is null || refreshToken.ExpireAt < DateTime.UtcNow)
            return RefreshTokenErrors.AlreadyExpired;

        var accessToken = _tokenProvider.GenerateAccessToken(refreshToken.User);
        
        refreshToken.Value = _tokenProvider.GenerateRefreshToken();
        refreshToken.ExpireAt = DateTime.UtcNow.AddDays(7);
        
        await _dbContext.SaveChangesAsync(ct);

        return new LoginWithRefreshTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Value
        };
    }
}