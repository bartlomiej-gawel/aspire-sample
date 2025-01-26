using ErrorOr;

namespace Sample.Modules.Organizations.Features.Employees;

internal static class EmployeeErrors
{
    public static readonly Error AlreadyActivated = Error.Conflict(
        "EmployeeErrors.AlreadyActivated",
        "Employee is already activated.");
}