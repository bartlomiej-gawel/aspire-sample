using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Shared.Infrastructure.Endpoints;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/users-module/users/{activationToken:guid}/activate", async (
                Guid activationToken,
                ISender sender) =>
            {
                var result = await sender.Send(new ActivateUserRequest(activationToken));
                return result.Match(
                    _ => Results.Ok(),
                    errors => errors.ToProblemResult());
            })
            .WithName("activate-user");
    }
}