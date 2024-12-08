using ErrorOr;

namespace Sample.Services.Organizations.Features.Organizations;

public static class OrganizationErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "OrganizationErrors.NotFound",
        "Organization with provided id was not found.");
    
    public static readonly Error AlreadyActivated = Error.Conflict(
        "OrganizationErrors.AlreadyActivated",
        "Organization with provided id is already activated.");
}