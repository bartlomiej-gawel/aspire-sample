using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.Users;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.ActivationTokens.ActivateUser;

public static class ActivateUserEndpoint
{
    public static IEndpointRouteBuilder MapActivateUserEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("api/users-service/activation-tokens/{activationToken:guid}/activate-user", async (
                Guid activationToken,
                UsersServiceDbContext dbContext,
                TimeProvider timeProvider,
                IPublishEndpoint publishEndpoint,
                CancellationToken cancellationToken) =>
            {
                var currentUtcDate = timeProvider.GetUtcNow().DateTime.ToUniversalTime();
                
                var userActivationToken = await dbContext.ActivationTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == activationToken, cancellationToken);

                if (userActivationToken is null)
                    return Results.NotFound("Activation token was not found.");

                if (userActivationToken.ExpireAt < currentUtcDate)
                    return Results.BadRequest("Activation token is expired.");

                if (userActivationToken.User.Status == UserStatus.Active)
                    return Results.BadRequest("User is already active.");
                
                userActivationToken.User.Status = UserStatus.Active;
                userActivationToken.User.UpdatedAt = currentUtcDate;
                
                dbContext.Users.Update(userActivationToken.User);
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