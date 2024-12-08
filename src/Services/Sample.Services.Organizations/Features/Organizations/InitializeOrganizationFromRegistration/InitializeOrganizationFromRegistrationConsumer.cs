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

            // location
            var locationId = Guid.CreateVersion7();
            
            // position
            var positionId = Guid.CreateVersion7();
            
            // employee
            var employeeId = context.Message.UserId;
            var employeeName = context.Message.UserName;
            var employeeSurname = context.Message.UserSurname;
            var employeeEmail = context.Message.UserEmail;
            var employeePhone = context.Message.UserPhone;
            
            // organization
            var organizationId = context.Message.OrganizationId;
            var organizationName = context.Message.OrganizationName;
            
            var employee = Employee.Initialize(
                employeeId,
                locationId,
                employeeName,
                employeeSurname,
                employeeEmail,
                employeePhone);

            var location = Location.Initialize(
                locationId,
                organizationId);

            var position = Position.Initialize(
                positionId,
                organizationId);

            var employeePosition = EmployeePosition.Initialize(
                employeeId,
                positionId);

            var organization = Organization.Initialize(
                organizationId,
                organizationName);
            
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