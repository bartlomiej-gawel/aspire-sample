using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.RefreshTokens;
using Sample.Services.Users.Shared;

namespace Sample.Services.Users.Features.Users.LoginUser;

public sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, ErrorOr<LoginUserResponse>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly TokenProvider _tokenProvider;

    public LoginUserEndpoint(
        UsersServiceDbContext dbContext,
        PasswordHasher passwordHasher,
        TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public override void Configure()
    {
        Post("api/users-service/users/login");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<LoginUserResponse>> ExecuteAsync(LoginUserRequest req, CancellationToken ct)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == req.Email, ct);
        if (user is null)
            return UserErrors.NotFound;

        if (user.Status != UserStatus.Active)
            return UserErrors.NotActivated;
        
        var isPasswordVerified = _passwordHasher.Verify(req.Password, user.Password);
        if (!isPasswordVerified)
            return UserErrors.IncorrectPassword;

        var accessToken = _tokenProvider.GenerateAccessToken(user);
        var refreshToken = new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = user.Id,
            Value = _tokenProvider.GenerateRefreshToken(),
            ExpireAt = DateTime.UtcNow.AddDays(7)
        };
        
        await _dbContext.RefreshTokens.AddAsync(refreshToken, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new LoginUserResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Value
        };
    }
}