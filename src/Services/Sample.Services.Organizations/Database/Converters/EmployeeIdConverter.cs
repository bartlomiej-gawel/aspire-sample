using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.Services.Organizations.Features.Employees;

namespace Sample.Services.Organizations.Database.Converters;

public sealed class EmployeeIdConverter : ValueConverter<EmployeeId, Guid>
{
    public EmployeeIdConverter() : base(
        employeeId => employeeId.Value,
        employeeId => EmployeeId.From(employeeId))
    {
    }
}