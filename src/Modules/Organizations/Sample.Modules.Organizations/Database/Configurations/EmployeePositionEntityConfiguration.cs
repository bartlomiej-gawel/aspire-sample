using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Organizations.Features.EmployeePositions;
using Sample.Modules.Organizations.Features.Employees;

namespace Sample.Modules.Organizations.Database.Configurations;

internal sealed class EmployeePositionEntityConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.PositionId });

        builder.Property(x => x.EmployeeId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId);

        builder.Property(x => x.PositionId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.Position)
            .WithMany()
            .HasForeignKey(x => x.PositionId);
    }
}