using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Shared.Infrastructure.Endpoints;

namespace Sample.Modules.Users.Features.RefreshTokens.RevokeRefreshTokens;

internal sealed class RevokeRefreshTokensEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/users-service/refresh-tokens/{userId:guid}/revoke", async (
                Guid userId,
                ISender sender) =>
            {
                var result = await sender.Send(new RevokeRefreshTokensRequest(userId));
                return result.Match(
                    _ => Results.Ok(),
                    errors => errors.ToProblemResult());
            })
            .WithName("revoke-refresh-tokens");
    }
}