using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Shared.Infrastructure.Endpoints;

namespace Sample.Modules.Users.Features.Users.RegisterUser;

internal sealed class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users-module/users/register", async (
                Request request,
                ISender sender) =>
            {
                var result = await sender.Send(new RegisterUserRequest(
                    request.Name,
                    request.Surname,
                    request.OrganizationName,
                    request.Email,
                    request.Phone,
                    request.Password,
                    request.RepeatPassword
                ));

                return result.Match(
                    _ => Results.Ok(),
                    errors => errors.ToProblemResult());
            })
            .WithName("register-user");
    }

    private sealed record Request(
        string Name,
        string Surname,
        string OrganizationName,
        string Email,
        string Phone,
        string Password,
        string RepeatPassword);
}