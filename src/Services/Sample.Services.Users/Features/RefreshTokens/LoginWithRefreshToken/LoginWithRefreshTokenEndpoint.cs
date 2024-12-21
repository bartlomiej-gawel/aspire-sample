using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Shared;

namespace Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;

public static class LoginWithRefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapLoginWithRefreshTokenEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("api/users-service/refresh-tokens/login", async (
                LoginWithRefreshTokenRequest request,
                IValidator<LoginWithRefreshTokenRequest> validator,
                UsersServiceDbContext dbContext,
                TokenProvider tokenProvider,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var currentUtcDate = timeProvider.GetUtcNow().DateTime.ToUniversalTime();
                
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var refreshToken = await dbContext.RefreshTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Value == request.RefreshToken, cancellationToken);

                if (refreshToken is null || refreshToken.ExpireAt < currentUtcDate)
                    return Results.BadRequest("Refresh token is invalid or expired.");

                var refreshTokenValue = TokenProvider.GenerateRefreshToken();
                if (string.IsNullOrEmpty(refreshTokenValue))
                    return Results.BadRequest("Failed to generate refresh token.");

                var accessTokenValue = tokenProvider.GenerateAccessToken(refreshToken.User);
                if (string.IsNullOrEmpty(accessTokenValue))
                    return Results.BadRequest("Failed to generate access token.");
                
                refreshToken.Value = refreshTokenValue;
                refreshToken.ExpireAt = DateTime.UtcNow.AddDays(7);
                
                dbContext.RefreshTokens.Update(refreshToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok(new LoginWithRefreshTokenResponse
                {
                    AccessToken = accessTokenValue,
                    RefreshToken = refreshTokenValue
                });
            })
            .AllowAnonymous()
            .WithName("login-with-refresh-token");

        return builder;
    }
}