using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.RefreshTokens;
using Sample.Modules.Users.Infrastructure.Jwt;

namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, ErrorOr<LoginUserResponse>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly JwtProvider _jwtProvider;

    public LoginUserEndpoint(
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
        Post("api/users-service/users/login");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<LoginUserResponse>> ExecuteAsync(LoginUserRequest req, CancellationToken ct)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == req.Email, ct);
        if (user is null)
            return UserErrors.NotFound;

        if (user.Status != UserStatus.Active)
            return UserErrors.NotActivated;

        var isPasswordVerifiedResult = UserPasswordHasher.Verify(req.Password, user.Password);
        if (isPasswordVerifiedResult.IsError)
            return isPasswordVerifiedResult.Errors;
        
        var accessTokenGenerationResult = _jwtProvider.GenerateAccessToken(user);
        if (accessTokenGenerationResult.IsError)
            return accessTokenGenerationResult.Errors;
        
        var refreshTokenGenerationResult = _jwtProvider.GenerateRefreshToken();
        if (refreshTokenGenerationResult.IsError)
            return refreshTokenGenerationResult.Errors;
        
        var refreshToken = RefreshToken.Create(
            user.Id,
            refreshTokenGenerationResult.Value,
            utcDateTime);
        
        await _dbContext.RefreshTokens.AddAsync(refreshToken, ct);
        await _dbContext.SaveChangesAsync(ct);
        
        var response = new LoginUserResponse
        {
            AccessToken = accessTokenGenerationResult.Value,
            RefreshToken = refreshTokenGenerationResult.Value
        };
        
        return response;
    }
}