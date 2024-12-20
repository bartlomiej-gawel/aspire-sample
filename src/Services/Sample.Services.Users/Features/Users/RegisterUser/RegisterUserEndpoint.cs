using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.ActivationTokens;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public static class RegisterUserEndpoint
{
    public static IEndpointRouteBuilder MapRegisterUserEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("api/users-service/users/register", async (
                RegisterUserRequest request,
                IValidator<RegisterUserRequest> validator,
                UsersServiceDbContext dbContext,
                IPublishEndpoint publishEndpoint,
                ActivationTokenLinkFactory activationTokenLinkFactory,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var currentUtcDate = timeProvider.GetUtcNow().DateTime.ToUniversalTime();
                
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var existingUser = await dbContext.Users
                    .Where(x => x.OrganizationName == request.OrganizationName || x.Email == request.Email)
                    .Select(x => new { x.OrganizationName, x.Email })
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                {
                    if (existingUser.OrganizationName == request.OrganizationName)
                        return Results.BadRequest("Organization name already exists.");

                    if (existingUser.Email == request.Email)
                        return Results.BadRequest("Email already exists.");
                }

                var hashedPassword = UserPasswordHasher.Hash(request.Password);
                if (string.IsNullOrEmpty(hashedPassword))
                    return Results.BadRequest("Provided password is invalid.");
                
                var user = new User
                {
                    Id = Guid.CreateVersion7(),
                    OrganizationId = Guid.CreateVersion7(),
                    OrganizationName = request.OrganizationName,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = hashedPassword,
                    Status = UserStatus.Inactive,
                    CreatedAt = currentUtcDate,
                    UpdatedAt = null
                };

                var activationToken = new ActivationToken
                {
                    Id = Guid.CreateVersion7(),
                    UserId = user.Id,
                    CreatedAt = currentUtcDate,
                    ExpireAt = currentUtcDate.AddDays(1)
                };
                
                var activationLink = activationTokenLinkFactory.CreateLink(activationToken);
                if (string.IsNullOrEmpty(activationLink))
                    return Results.BadRequest("Failed to create activation link.");

                await dbContext.Users.AddAsync(user, cancellationToken);
                await dbContext.ActivationTokens.AddAsync(activationToken, cancellationToken);

                await publishEndpoint.Publish(new UserRegistered(
                        user.Id,
                        user.Name,
                        user.Surname,
                        user.Email,
                        user.Phone,
                        user.OrganizationId,
                        user.OrganizationName,
                        activationLink),
                    cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok();
            })
            .AllowAnonymous()
            .WithName("register-user");

        return builder;
    }
}