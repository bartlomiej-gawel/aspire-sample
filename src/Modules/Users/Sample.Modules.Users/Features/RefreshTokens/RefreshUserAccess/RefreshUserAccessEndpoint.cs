using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Shared.Infrastructure.Endpoints;

namespace Sample.Modules.Users.Features.RefreshTokens.RefreshUserAccess;

internal sealed class RefreshUserAccessEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users-module/refresh-tokens/refresh", async (
                Request request,
                ISender sender) =>
            {
                var result = await sender.Send(new RefreshUserAccessRequest(request.RefreshToken));
                return result.Match(
                    _ => Results.Ok(),
                    errors => errors.ToProblemResult());
            })
            .WithName("refresh-user-access");
    }

    private sealed record Request(string RefreshToken);
}