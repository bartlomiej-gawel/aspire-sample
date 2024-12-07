using MassTransit;
using Sample.Services.Organizations.Database;
using Sample.Services.Organizations.Features.Employees;
using Sample.Services.Organizations.Features.Locations;
using Sample.Services.Organizations.Features.Positions;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Organizations.Features.Organizations.InitializeOrganizationFromRegistration;

public sealed class InitializeOrganizationFromRegistrationConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<InitializeOrganizationFromRegistrationConsumer> _logger;
    private readonly OrganizationsServiceDbContext _dbContext;

    public InitializeOrganizationFromRegistrationConsumer(
        ILogger<InitializeOrganizationFromRegistrationConsumer> logger,
        OrganizationsServiceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        try
        {
            _logger.LogInformation("Initializing organization from registration.");

            var locationId = Guid.CreateVersion7();
            var positionId = Guid.CreateVersion7();

            var employee = new Employee
            {
                Id = context.Message.UserId,
                LocationId = locationId,
                Name = context.Message.UserName,
                Surname = context.Message.UserSurname,
                Email = context.Message.UserEmail,
                Phone = context.Message.UserPhone,
                Status = EmployeeStatus.Inactive
            };

            var location = new Location
            {
                Id = locationId,
                OrganizationId = context.Message.OrganizationId,
                Name = "Default",
                Address = LocationAddress.Initialize(),
                Employees = [employee]
            };

            var position = new Position
            {
                Id = positionId,
                OrganizationId = context.Message.OrganizationId
            };

            var employeePosition = new EmployeePosition
            {
                EmployeeId = context.Message.UserId,
                PositionId = positionId
            };

            var organization = new Organization
            {
                Id = context.Message.OrganizationId,
                Name = context.Message.OrganizationName,
                Status = OrganizationStatus.Inactive,
                Locations = [location],
                Positions = [position]
            };
            
            await _dbContext.Employees.AddAsync(employee, context.CancellationToken);
            await _dbContext.Locations.AddAsync(location, context.CancellationToken);
            await _dbContext.Positions.AddAsync(position, context.CancellationToken);
            await _dbContext.EmployeePositions.AddAsync(employeePosition, context.CancellationToken);
            await _dbContext.Organizations.AddAsync(organization, context.CancellationToken);
            await _dbContext.SaveChangesAsync(context.CancellationToken);

            _logger.LogInformation("Organization initialized successfully.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while initializing organization from registration.");
        }
    }
}