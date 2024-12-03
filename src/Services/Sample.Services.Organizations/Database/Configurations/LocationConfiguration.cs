using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Organizations.Database.Converters;
using Sample.Services.Organizations.Features.Locations;

namespace Sample.Services.Organizations.Database.Configurations;

public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<LocationIdConverter>();

        builder.Property(x => x.OrganizationId)
            .HasConversion<OrganizationIdConverter>();

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Location)
            .HasForeignKey(x => x.LocationId);
    }
}