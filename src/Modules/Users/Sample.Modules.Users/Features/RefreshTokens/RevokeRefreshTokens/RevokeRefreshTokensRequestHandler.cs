using System.Security.Claims;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensRequestHandler : IRequestHandler<RevokeRefreshTokensRequest, ErrorOr<Success>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RevokeRefreshTokensRequestHandler(
        UsersModuleDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<Success>> Handle(RevokeRefreshTokensRequest request, CancellationToken cancellationToken)
    {
        var nameIdentifierFromClaims = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(nameIdentifierFromClaims))
            return RefreshTokenErrors.UnableToGetClaimsValueFromHttpContext;

        var isValidUserId = Guid.TryParse(nameIdentifierFromClaims, out var currentUserId);
        if (!isValidUserId)
            return RefreshTokenErrors.UnableToParseUserFromHttpContext;

        if (currentUserId != request.UserId)
            return RefreshTokenErrors.UnauthorizedUserToRevokeTokens;

        await _dbContext.RefreshTokens
            .Where(x => x.UserId == request.UserId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success;
    }
}