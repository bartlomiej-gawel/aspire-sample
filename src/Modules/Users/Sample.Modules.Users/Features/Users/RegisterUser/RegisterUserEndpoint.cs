using ErrorOr;
using FastEndpoints;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Shared.Messages.Modules.Users;

namespace Sample.Modules.Users.Features.Users.RegisterUser;

internal sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ErrorOr<Ok>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly TimeProvider _timeProvider;
    private readonly ActivationTokenLinkFactory _activationTokenLinkFactory;
    private readonly IPublishEndpoint _publishEndpoint;

    public RegisterUserEndpoint(
        UsersModuleDbContext dbContext,
        TimeProvider timeProvider,
        ActivationTokenLinkFactory activationTokenLinkFactory,
        IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
        _activationTokenLinkFactory = activationTokenLinkFactory;
        _publishEndpoint = publishEndpoint;
    }

    public override void Configure()
    {
        Post("api/users-service/users/register");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<Ok>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var utcDateTime = _timeProvider.GetUtcNow().UtcDateTime;

        var existingUser = await _dbContext.Users
            .Where(x => x.OrganizationName == req.OrganizationName || x.Email == req.Email)
            .Select(x => new { x.OrganizationName, x.Email })
            .FirstOrDefaultAsync(ct);

        if (existingUser != null)
        {
            if (existingUser.OrganizationName == req.OrganizationName)
                return UserErrors.OrganizationNameAlreadyExists;

            if (existingUser.Email == req.Email)
                return UserErrors.EmailAlreadyInUse;
        }

        var hashedPasswordResult = UserPasswordHasher.Hash(req.Password);
        if (hashedPasswordResult.IsError)
            return hashedPasswordResult.Errors;

        var user = Users.User.RegisterOrganizationAdmin(
            req.OrganizationName,
            req.Name,
            req.Surname,
            req.Email,
            req.Phone,
            hashedPasswordResult.Value);

        var activationToken = ActivationToken.Create(
            user.Id,
            utcDateTime);

        var activationTokenLinkResult = _activationTokenLinkFactory.CreateLink(activationToken);
        if (activationTokenLinkResult.IsError)
            return activationTokenLinkResult.Errors;

        await _dbContext.Users.AddAsync(user, ct);
        await _dbContext.ActivationTokens.AddAsync(activationToken, ct);

        await _publishEndpoint.Publish(new UserRegistered(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.OrganizationId,
                user.OrganizationName,
                activationTokenLinkResult.Value),
            ct);

        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}