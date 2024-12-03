using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Database.Converters;

public sealed class OrganizationIdConverter : ValueConverter<OrganizationId, Guid>
{
    public OrganizationIdConverter() : base(
        organizationId => organizationId.Value,
        organizationId => OrganizationId.From(organizationId))
    {
    }
}