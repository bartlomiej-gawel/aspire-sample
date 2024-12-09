using ErrorOr;
using FastEndpoints;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest, ErrorOr<IResult>>
{
    private readonly UsersServiceDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public RegisterUserEndpoint(
        UsersServiceDbContext dbContext, 
        IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
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
        
        var newUser = new User
        {
            OrganizationName = req.OrganizationName,
            Name = req.Name,
            Surname = req.Surname,
            Email = req.Email,
            Phone = req.Phone,
            Password = req.Password,
            Status = UserStatus.Inactive
        };
        
        await _dbContext.Users.AddAsync(newUser, ct);
        await _publishEndpoint.Publish(new UserRegistered(
                newUser.Id,
                newUser.Name,
                newUser.Surname,
                newUser.Email,
                newUser.Phone,
                newUser.OrganizationId,
                newUser.OrganizationName),
            ct);
        
        await _dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}