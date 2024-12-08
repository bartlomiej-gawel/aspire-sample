using ErrorOr;

namespace Sample.Services.Organizations.Features.Employees;

public static class EmployeeErrors
{
    public static readonly Error AlreadyActivated = Error.Conflict(
        "EmployeeErrors.AlreadyActivated",
        "Organization employee with provided id is already activated.");
}