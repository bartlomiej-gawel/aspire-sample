using ErrorOr;
using FastEndpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.VerificationTokens;
using Sample.Shared.Messages.UsersService;
using static Sample.Services.Users.Features.Users.User;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ErrorOr<IResult>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly TimeProvider _timeProvider;
    private readonly VerificationTokenLinkFactory _verificationTokenLinkFactory;

    public RegisterUserEndpoint(
        UsersServiceDbContext dbContext,
        IPublishEndpoint publishEndpoint,
        TimeProvider timeProvider,
        VerificationTokenLinkFactory verificationTokenLinkFactory)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
        _timeProvider = timeProvider;
        _verificationTokenLinkFactory = verificationTokenLinkFactory;
    }

    public override void Configure()
    {
        Post("api/users-service/users/register");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<IResult>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var existingUser = await _dbContext.Users
            .Where(x => x.OrganizationName == req.OrganizationName || x.Email == req.Email)
            .Select(x => new { x.OrganizationName, x.Email })
            .FirstOrDefaultAsync(ct);

        if (existingUser != null)
        {
            if (existingUser.OrganizationName == req.OrganizationName)
                return UserErrors.OrganizationNameAlreadyExists;

            if (existingUser.Email == req.Email)
                return UserErrors.EmailAlreadyExists;
        }

        var user = Register(
            req.OrganizationName,
            req.Name,
            req.Surname,
            req.Email,
            req.Phone,
            UserPasswordHasher.Hash(req.Password));

        var verificationToken = VerificationToken.Generate(
            user.Id,
            _timeProvider.GetUtcNow().DateTime);

        var verificationLinkCreateResult = _verificationTokenLinkFactory.CreateLink(verificationToken);
        if (verificationLinkCreateResult.IsError)
            return verificationLinkCreateResult.FirstError;
        
        await _dbContext.Users.AddAsync(user, ct);
        await _dbContext.VerificationTokens.AddAsync(verificationToken, ct);

        await _publishEndpoint.Publish(new UserRegistered(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.OrganizationId,
                user.OrganizationName,
                verificationLinkCreateResult.Value),
            ct);

        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}