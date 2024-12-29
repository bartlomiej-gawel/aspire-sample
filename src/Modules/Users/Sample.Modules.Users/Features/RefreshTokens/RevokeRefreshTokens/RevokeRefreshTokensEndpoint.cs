using System.Security.Claims;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensEndpoint : Endpoint<RevokeRefreshTokensRequest, ErrorOr<Ok>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RevokeRefreshTokensEndpoint(
        UsersModuleDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Delete("api/users-service/refresh-tokens/{UserId}/revoke-tokens");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<Ok>> ExecuteAsync(RevokeRefreshTokensRequest req, CancellationToken ct)
    {
        var nameIdentifierFromClaims = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(nameIdentifierFromClaims))
            return RefreshTokenErrors.UnableToGetClaimsValueFromHttpContext;
        
        var isValidUserId = Guid.TryParse(nameIdentifierFromClaims, out var currentUserId);
        if (!isValidUserId)
            return RefreshTokenErrors.UnableToParseUserFromHttpContext;

        if (currentUserId != req.UserId)
            return RefreshTokenErrors.UnauthorizedUserToRevokeTokens;
        
        await _dbContext.RefreshTokens
            .Where(x => x.UserId == req.UserId)
            .ExecuteDeleteAsync(ct);
        
        return TypedResults.Ok();
    }
}