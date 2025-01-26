using ErrorOr;
using MediatR;
using Sample.Modules.Organizations.Database;
using Sample.Modules.Organizations.Features.EmployeePositions;
using Sample.Modules.Organizations.Features.Employees;
using Sample.Modules.Organizations.Features.Locations;
using Sample.Modules.Organizations.Features.Positions;

namespace Sample.Modules.Organizations.Features.Organizations.InitializeOrganizationFromRegistration;

internal sealed class InitializeOrganizationFromRegistrationRequestHandler : IRequestHandler<InitializeOrganizationFromRegistrationRequest, ErrorOr<Success>>
{
    private readonly OrganizationsModuleDbContext _dbContext;

    public InitializeOrganizationFromRegistrationRequestHandler(OrganizationsModuleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(InitializeOrganizationFromRegistrationRequest request, CancellationToken cancellationToken)
    {
        // location
        var locationId = Guid.CreateVersion7();

        // position
        var positionId = Guid.CreateVersion7();

        // employee
        var employeeId = request.UserId;
        var employeeName = request.Name;
        var employeeSurname = request.Surname;
        var employeeEmail = request.Email;
        var employeePhone = request.Phone;

        // organization
        var organizationId = request.OrganizationId;
        var organizationName = request.OrganizationName;

        var employee = Employee.InitializeOrganizationAdmin(
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

        await _dbContext.Employees.AddAsync(employee, cancellationToken);
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.Positions.AddAsync(position, cancellationToken);
        await _dbContext.EmployeePositions.AddAsync(employeePosition, cancellationToken);
        await _dbContext.Organizations.AddAsync(organization, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}