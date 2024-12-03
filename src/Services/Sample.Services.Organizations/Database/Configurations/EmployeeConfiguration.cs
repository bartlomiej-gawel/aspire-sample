using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Organizations.Database.Converters;
using Sample.Services.Organizations.Features.Employees;

namespace Sample.Services.Organizations.Database.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<EmployeeIdConverter>();

        builder.Property(x => x.LocationId)
            .HasConversion<LocationIdConverter>();
    }
}