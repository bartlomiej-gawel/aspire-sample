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
    private readonly TokenProvider _tokenProvider;

    public LoginUserEndpoint(
        UsersServiceDbContext dbContext,
        TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
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

        var isPasswordVerified = UserPasswordHasher.Verify(req.Password, user.Password);
        if (!isPasswordVerified)
            return UserErrors.IncorrectPassword;

        var refreshToken = RefreshToken.Create(
            user.Id,
            TokenProvider.GenerateRefreshToken());

        await _dbContext.RefreshTokens.AddAsync(refreshToken, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new LoginUserResponse
        {
            AccessToken = _tokenProvider.GenerateAccessToken(user),
            RefreshToken = refreshToken.Value
        };
    }
}