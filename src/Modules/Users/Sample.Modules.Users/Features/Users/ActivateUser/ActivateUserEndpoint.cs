using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Shared.Messages.Modules.Users;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserEndpoint : Endpoint<ActivateUserRequest, ErrorOr<Ok>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly TimeProvider _timeProvider;

    public ActivateUserEndpoint(
        UsersModuleDbContext dbContext,
        TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public override void Configure()
    {
        Put("api/users-service/users/{ActivationToken}/activate");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<Ok>> ExecuteAsync(ActivateUserRequest req, CancellationToken ct)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;

        var userActivationToken = await _dbContext.ActivationTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == req.ActivationToken, ct);

        if (userActivationToken is null)
            return ActivationTokenErrors.NotFound;

        if (userActivationToken.ExpireAt < utcDateTime)
            return ActivationTokenErrors.AlreadyExpired;

        var userActivationResult = userActivationToken.User.Activate();
        if (userActivationResult.IsError)
            return userActivationResult.Errors;

        _dbContext.ActivationTokens.Remove(userActivationToken);

        await new UserRegistrationConfirmed(
                userActivationToken.User.Id,
                userActivationToken.User.OrganizationId)
            .PublishAsync(Mode.WaitForNone, cancellation: ct);
        
        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}