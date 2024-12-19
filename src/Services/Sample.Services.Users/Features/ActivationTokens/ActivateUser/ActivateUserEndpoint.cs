using ErrorOr;
using FastEndpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.ActivationTokens.ActivateUser;

public sealed class ActivateUserEndpoint : Endpoint<ActivateUserRequest, ErrorOr<IResult>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly IPublishEndpoint _publishEndpoint;

    public ActivateUserEndpoint(
        UsersServiceDbContext dbContext,
        TimeProvider timeProvider,
        IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
        _publishEndpoint = publishEndpoint;
    }

    public override void Configure()
    {
        Put("{ActivationToken}/activate");
        Group<ActivationTokenEndpointsGroup>();
        AllowAnonymous();
        Description(x => x.WithName("ActivateUser"), clearDefaults: true);
    }

    public override async Task<ErrorOr<IResult>> ExecuteAsync(ActivateUserRequest req, CancellationToken ct)
    {
        var activationToken = await _dbContext.ActivationTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == req.ActivationToken, ct);

        var now = _timeProvider.GetUtcNow().DateTime.ToUniversalTime();
        
        if (activationToken is null || activationToken.ExpireAt < now)
            return ActivationTokenErrors.TokenExpired;

        var userActivationResult = activationToken.User.Activate();
        if (userActivationResult.IsError)
            return userActivationResult.FirstError;

        _dbContext.ActivationTokens.Remove(activationToken);

        await _publishEndpoint.Publish(new UserRegistrationConfirmed(
                activationToken.User.Id,
                activationToken.User.OrganizationId),
            ct);

        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}