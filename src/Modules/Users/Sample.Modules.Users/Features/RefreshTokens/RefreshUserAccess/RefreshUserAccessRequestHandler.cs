using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Infrastructure.Jwt;

namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessRequestHandler : IRequestHandler<RefreshUserAccessRequest, ErrorOr<RefreshUserAccessResponse>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly JwtProvider _jwtProvider;

    public RefreshUserAccessRequestHandler(
        UsersModuleDbContext dbContext,
        TimeProvider timeProvider,
        JwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<RefreshUserAccessResponse>> Handle(RefreshUserAccessRequest request, CancellationToken cancellationToken)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;
        
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Value == request.RefreshToken, cancellationToken);
        
        if (refreshToken is null)
            return RefreshTokenErrors.NotFound;

        if (refreshToken.ExpireAt < utcDateTime)
            return RefreshTokenErrors.AlreadyExpired;
        
        var generatedRefreshToken = _jwtProvider.GenerateRefreshToken();
        if (generatedRefreshToken.IsError)
            return generatedRefreshToken.Errors;
        
        var generatedAccessToken = _jwtProvider.GenerateAccessToken(refreshToken.User);
        if (generatedAccessToken.IsError)
            return generatedAccessToken.Errors;

        refreshToken.Update(generatedRefreshToken.Value);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var response = new RefreshUserAccessResponse(
            generatedAccessToken.Value,
            generatedRefreshToken.Value);

        return response;
    }
}