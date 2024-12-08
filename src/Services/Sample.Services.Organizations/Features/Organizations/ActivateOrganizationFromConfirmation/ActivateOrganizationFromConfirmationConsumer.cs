using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sample.Services.Organizations.Database;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Organizations.Features.Organizations.ActivateOrganizationFromConfirmation;

public sealed class ActivateOrganizationFromConfirmationConsumer : IConsumer<UserRegistrationConfirmed>
{
    private readonly ILogger<ActivateOrganizationFromConfirmationConsumer> _logger;
    private readonly OrganizationsServiceDbContext _dbContext;

    public ActivateOrganizationFromConfirmationConsumer(
        ILogger<ActivateOrganizationFromConfirmationConsumer> logger,
        OrganizationsServiceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserRegistrationConfirmed> context)
    {
        try
        {
            _logger.LogInformation("Activating organization from registration confirmation.");

            var location = await _dbContext.Locations
                .Include(x => x.Organization)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x =>
                        x.OrganizationId == context.Message.OrganizationId &&
                        x.Employees.Any(e => e.Id == context.Message.UserId),
                    context.CancellationToken);
            
            if (location is null)
            {
                _logger.LogError("Organization and employee was not found to activate.");
                return;
            }
            
            var organizationActivateResult = location.Organization.Activate();
            if (organizationActivateResult.IsError)
            {
                _logger.LogError("{ErrorDescription}", organizationActivateResult.FirstError.Description);
                return;
            }
            
            var employee = location.Employees.First(e => e.Id == context.Message.UserId);
            var employeeActivateResult = employee.Activate();
            if (employeeActivateResult.IsError)
            {
                _logger.LogError("{ErrorDescription}", employeeActivateResult.FirstError.Description);
                return;
            }
            
            await _dbContext.SaveChangesAsync(context.CancellationToken);
            
            _logger.LogInformation("Organization and employee activated successfully.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while activating organization from registration confirmation.");
        }
    }
}