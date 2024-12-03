using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Organizations.Database.Converters;
using Sample.Services.Organizations.Features.Employees;

namespace Sample.Services.Organizations.Database.Configurations;

public sealed class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.HasKey(x => new
        {
            x.EmployeeId,
            x.PositionId
        });

        builder.Property(x => x.EmployeeId)
            .HasConversion<EmployeeIdConverter>();

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId);

        builder.Property(x => x.PositionId)
            .HasConversion<PositionIdConverter>();

        builder.HasOne(x => x.Position)
            .WithMany()
            .HasForeignKey(x => x.PositionId);
    }
}