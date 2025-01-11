using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Shared.Contracts.Users;

namespace Sample.Modules.Users.Features.Users.ActivateUser;

internal sealed class ActivateUserRequestHandler : IRequestHandler<ActivateUserRequest, ErrorOr<Success>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly TimeProvider _timeProvider;

    public ActivateUserRequestHandler(
        UsersModuleDbContext dbContext,
        IPublisher publisher,
        TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _timeProvider = timeProvider;
    }

    public async Task<ErrorOr<Success>> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;

        var userActivationToken = await _dbContext.ActivationTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.ActivationToken, cancellationToken);

        if (userActivationToken is null)
            return ActivationTokenErrors.NotFound;

        if (userActivationToken.ExpireAt < utcDateTime)
            return ActivationTokenErrors.AlreadyExpired;

        var userActivationResult = userActivationToken.User.Activate();
        if (userActivationResult.IsError)
            return userActivationResult.Errors;

        _dbContext.ActivationTokens.Remove(userActivationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new UserRegistrationConfirmedNotification(
                userActivationToken.User.Id,
                userActivationToken.User.OrganizationId),
            cancellationToken);

        return Result.Success;
    }
}