using System.Security.Claims;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;

namespace Sample.Services.Users.Features.RefreshTokens.RevokeRefreshTokens;

public sealed class RevokeRefreshTokensEndpoint : Endpoint<RevokeRefreshTokensRequest, ErrorOr<IResult>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RevokeRefreshTokensEndpoint(
        UsersServiceDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Delete("api/users-service/refresh-tokens/{UserId}/revoke");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<IResult>> ExecuteAsync(RevokeRefreshTokensRequest req, CancellationToken ct)
    {
        if (req.UserId != GetCurrentUserId())
            return RefreshTokenErrors.UnauthorizedRevoke;
        
        await _dbContext.RefreshTokens
            .Where(x => x.UserId == req.UserId)
            .ExecuteDeleteAsync(ct);

        return TypedResults.Ok();
    }

    private Guid? GetCurrentUserId()
    {
        return Guid.TryParse(_httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? userId
                : null;
    }
}