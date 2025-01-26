using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Shared.Infrastructure.Endpoints;

namespace Sample.Modules.Users.Features.Users.LoginUser;

internal sealed class LoginUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users-module/users/login", async (
                Request request,
                ISender sender) =>
            {
                var result = await sender.Send(new LoginUserRequest(
                    request.Email,
                    request.Password));

                return result.Match(
                    _ => Results.Ok(result.Value),
                    errors => errors.ToProblemResult());
            })
            .WithName("login-user");
    }

    private sealed record Request(
        string Email,
        string Password);
}