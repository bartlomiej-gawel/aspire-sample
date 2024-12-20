using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;

namespace Sample.Services.Users.Features.RefreshTokens.RevokeRefreshTokens;

public static class RevokeRefreshTokensEndpoint
{
    public static IEndpointRouteBuilder MapEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("api/users-service/refresh-tokens/{userId:guid}/revoke", async (
                Guid userId,
                UsersServiceDbContext dbContext,
                IHttpContextAccessor httpContextAccessor,
                CancellationToken cancellationToken) =>
            {
                var currentUserId = GetCurrentUserId(httpContextAccessor);
                if (currentUserId != userId)
                    return Results.Unauthorized();

                await dbContext.RefreshTokens
                    .Where(x => x.UserId == userId)
                    .ExecuteDeleteAsync(cancellationToken);

                return Results.Ok();
            })
            .AllowAnonymous()
            .WithName("revoke-refresh-tokens");

        return builder;
    }

    private static Guid? GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
    {
        return Guid.TryParse(httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            ? userId
            : null;
    }
}