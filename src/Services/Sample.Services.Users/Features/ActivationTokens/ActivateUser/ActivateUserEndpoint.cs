using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.ActivationTokens.ActivateUser;

public static class ActivateUserEndpoint
{
    public static IEndpointRouteBuilder MapEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("api/users-service/activation-tokens/{activationToken:guid}/activate-user", async (
                Guid activationToken,
                UsersServiceDbContext dbContext,
                TimeProvider timeProvider,
                IPublishEndpoint publishEndpoint,
                CancellationToken cancellationToken) =>
            {
                var userActivationToken = await dbContext.ActivationTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == activationToken, cancellationToken);

                if (userActivationToken is null)
                    return Results.BadRequest("Invalid activation token.");

                if (userActivationToken.ExpireAt < timeProvider.GetUtcNow().DateTime.ToUniversalTime())
                    return Results.BadRequest("Activation token is expired.");

                userActivationToken.User.Activate();

                dbContext.ActivationTokens.Remove(userActivationToken);

                await publishEndpoint.Publish(new UserRegistrationConfirmed(
                        userActivationToken.User.Id,
                        userActivationToken.User.OrganizationId),
                    cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok();
            })
            .AllowAnonymous()
            .WithName("activate-user");

        return builder;
    }
}