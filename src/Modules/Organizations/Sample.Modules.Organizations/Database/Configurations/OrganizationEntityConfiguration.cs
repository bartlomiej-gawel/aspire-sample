using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Organizations.Features.Organizations;

namespace Sample.Modules.Organizations.Database.Configurations;

internal sealed class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.OrganizationId);

        builder.Property(x => x.OrganizationId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.OrganizationName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(x => x.Locations)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);

        builder.HasMany(x => x.Positions)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);
    }
}