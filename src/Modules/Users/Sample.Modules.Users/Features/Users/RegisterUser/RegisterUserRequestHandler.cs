using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Modules.Users.Database;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Shared.Contracts.Users;

namespace Sample.Modules.Users.Features.Users.RegisterUser;

internal sealed class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, ErrorOr<Success>>
{
    private readonly UsersModuleDbContext _dbContext;
    private readonly ActivationTokenLinkFactory _activationTokenLinkFactory;
    private readonly IPublisher _publisher;

    public RegisterUserRequestHandler(
        UsersModuleDbContext dbContext,
        ActivationTokenLinkFactory activationTokenLinkFactory,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _activationTokenLinkFactory = activationTokenLinkFactory;
        _publisher = publisher;
    }

    public async Task<ErrorOr<Success>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users
            .Where(x => x.OrganizationName == request.OrganizationName || x.Email == request.Email)
            .Select(x => new { x.OrganizationName, x.Email })
            .FirstOrDefaultAsync(cancellationToken);

        if (existingUser != null)
        {
            if (existingUser.OrganizationName == request.OrganizationName)
                return UserErrors.OrganizationNameAlreadyExists;

            if (existingUser.Email == request.Email)
                return UserErrors.EmailAlreadyExists;
        }

        var hashedPassword = UserPasswordHasher.Hash(request.Password);
        if (hashedPassword.IsError)
            return hashedPassword.Errors;

        var user = User.RegisterOrganizationAdmin(
            request.OrganizationName,
            request.Name,
            request.Surname,
            request.Email,
            request.Phone,
            hashedPassword.Value);

        var activationToken = ActivationToken.CreateToken(user.Id);

        var activationTokenLink = _activationTokenLinkFactory.CreateLink(activationToken);
        if (activationTokenLink.IsError)
            return activationTokenLink.Errors;

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.ActivationTokens.AddAsync(activationToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new UserRegisteredNotification(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.OrganizationId,
                user.OrganizationName,
                activationTokenLink.Value),
            cancellationToken);

        return Result.Success;
    }
}