using FastEndpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Users.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Users.Features.Users.RegisterUser;

public sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest>
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
        Post("api/users-service/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var isOrganizationExists = await _dbContext.Users.AnyAsync(x => x.OrganizationName == req.OrganizationName, ct);
        if (isOrganizationExists) 
            ThrowError("Organization with provided name already exists");
        
        var isEmailExists = await _dbContext.Users.AnyAsync(x => x.Email == req.Email, ct);
        if (isEmailExists) 
            ThrowError("User with provided email already exists");

        var newUser = new User
        {
            Id = Guid.CreateVersion7(),
            OrganizationId = Guid.CreateVersion7(),
            OrganizationName = req.OrganizationName,
            Name = req.Name,
            Surname = req.Surname,
            Email = req.Email,
            Phone = req.Phone,
            Password = req.Password
        };

        await _dbContext.Users.AddAsync(newUser, ct);

        await _publishEndpoint.Publish(new UserRegistered(
                newUser.Id,
                newUser.OrganizationId,
                newUser.OrganizationName,
                newUser.Name,
                newUser.Surname,
                newUser.Email,
                newUser.Phone),
            ct);
        
        await _dbContext.SaveChangesAsync(ct);
    }
}