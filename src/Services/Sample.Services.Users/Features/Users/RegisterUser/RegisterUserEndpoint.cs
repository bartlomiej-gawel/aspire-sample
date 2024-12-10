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
    private readonly UserPasswordHasher _userPasswordHasher;
    private readonly IPublishEndpoint _publishEndpoint;

    public RegisterUserEndpoint(
        UsersServiceDbContext dbContext, 
        UserPasswordHasher userPasswordHasher,
        IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _userPasswordHasher = userPasswordHasher;
        _publishEndpoint = publishEndpoint;
    }

    public override void Configure()
    {
        Post("api/users-service/users/register");
        AllowAnonymous();
    }

    public override async Task<ErrorOr<IResult>> ExecuteAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var isOrganizationExists = await _dbContext.Users.AnyAsync(x => x.OrganizationName == req.OrganizationName, ct);
        if (isOrganizationExists)
            return UserErrors.OrganizationNameAlreadyExists;
        
        var isEmailExists = await _dbContext.Users.AnyAsync(x => x.Email == req.Email, ct);
        if (isEmailExists)
            return UserErrors.EmailAlreadyExists;
        
        var user = Register(
            req.OrganizationName,
            req.Name,
            req.Surname,
            req.Email,
            req.Phone,
            _userPasswordHasher.Hash(req.Password));

        var verificationToken = new VerificationToken
        {
            Id = default,
            UserId = default,
            User = null,
            CreatedAt = default,
            ExpireAt = default
        };

        // var newUser = new User
        // {
        //     OrganizationName = req.OrganizationName,
        //     Name = req.Name,
        //     Surname = req.Surname,
        //     Email = req.Email,
        //     Phone = req.Phone,
        //     Password = req.Password,
        //     Status = UserStatus.Inactive
        // };
        
        await _dbContext.Users.AddAsync(user, ct);
        await _publishEndpoint.Publish(new UserRegistered(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.OrganizationId,
                user.OrganizationName),
            ct);
        
        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}