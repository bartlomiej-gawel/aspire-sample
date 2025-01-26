using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Organizations.Features.Positions;

namespace Sample.Modules.Organizations.Database.Configurations;

internal sealed class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.OrganizationId)
            .ValueGeneratedNever()
            .IsRequired();
    }
}