using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.RefreshTokens;
using Sample.Services.Users.Shared;

namespace Sample.Services.Users.Features.Users.LoginUser;

public static class LoginUserEndpoint
{
    public static IEndpointRouteBuilder MapLoginUserEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("api/users-service/users/login", async (
                LoginUserRequest request,
                IValidator<LoginUserRequest> validator,
                UsersServiceDbContext dbContext,
                TokenProvider tokenProvider,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var currentUtcDate = timeProvider.GetUtcNow().DateTime.ToUniversalTime();
                
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
                if (user is null)
                    return Results.NotFound("User with provided email was not found.");

                if (user.Status != UserStatus.Active)
                    return Results.BadRequest("User is not activated.");

                var isPasswordVerified = UserPasswordHasher.Verify(request.Password, user.Password);
                if (!isPasswordVerified)
                    return Results.BadRequest("Provided password is incorrect.");

                var accessTokenValue = tokenProvider.GenerateAccessToken(user);
                if (string.IsNullOrEmpty(accessTokenValue))
                    return Results.BadRequest("Failed to generate access token.");
                
                var refreshTokenValue = TokenProvider.GenerateRefreshToken();
                if (string.IsNullOrEmpty(refreshTokenValue))
                    return Results.BadRequest("Failed to generate refresh token.");

                var refreshToken = new RefreshToken
                {
                    Id = Guid.CreateVersion7(),
                    UserId = user.Id,
                    Value = refreshTokenValue,
                    ExpireAt = currentUtcDate.AddDays(7)
                };

                await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok(new LoginUserResponse
                {
                    AccessToken = accessTokenValue,
                    RefreshToken = refreshTokenValue
                });
            })
            .AllowAnonymous()
            .WithName("login-user");

        return builder;
    }
}