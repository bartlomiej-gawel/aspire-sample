using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.RefreshTokens;
using Sample.Modules.Users.Infrastructure.Jwt;

namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, ErrorOr<LoginUserResponse>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly JwtProvider _jwtProvider;

    public LoginUserRequestHandler(
        UsersModuleDbContext dbContext,
        JwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user is null)
            return UserErrors.NotFound;

        if (user.Status != UserStatus.Active)
            return UserErrors.NotActivated;

        var isPasswordVerified = UserPasswordHasher.Verify(request.Password, user.Password);
        if (isPasswordVerified.IsError)
            return isPasswordVerified.Errors;

        if (!isPasswordVerified.Value)
            return UserErrors.InvalidPassword;

        var generatedAccessToken = _jwtProvider.GenerateAccessToken(user);
        if (generatedAccessToken.IsError)
            return generatedAccessToken.Errors;

        var generatedRefreshToken = _jwtProvider.GenerateRefreshToken();
        if (generatedRefreshToken.IsError)
            return generatedRefreshToken.Errors;

        var refreshToken = RefreshToken.Create(
            user.Id,
            generatedRefreshToken.Value);

        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = new LoginUserResponse(
            generatedAccessToken.Value,
            generatedRefreshToken.Value);

        return response;
    }
}