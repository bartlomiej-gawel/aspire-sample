using ErrorOr;
using FastEndpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.VerificationTokens.VerifyUserRegistration;

public sealed class VerifyUserRegistrationEndpoint : Endpoint<VerifyUserRegistrationRequest, ErrorOr<IResult>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly IPublishEndpoint _publishEndpoint;

    public VerifyUserRegistrationEndpoint(
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
        Put("/api/users-service/verification-tokens/verify/{VerificationToken}");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<IResult>> ExecuteAsync(VerifyUserRegistrationRequest req, CancellationToken ct)
    {
        var verificationToken = await _dbContext.VerificationTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == req.VerificationToken, ct);

        if (verificationToken is null || verificationToken.ExpireAt < _timeProvider.GetUtcNow().DateTime)
            return VerificationTokenErrors.TokenExpired;
        
        var userActivationResult = verificationToken.User.Activate();
        if (userActivationResult.IsError)
            return userActivationResult.FirstError;
        
        _dbContext.VerificationTokens.Remove(verificationToken);

        await _publishEndpoint.Publish(new UserRegistrationConfirmed(
                verificationToken.User.Id,
                verificationToken.User.OrganizationId),
            ct);

        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}