using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Organizations.Database.Converters;
using Sample.Services.Organizations.Features.Organizations;

namespace Sample.Services.Organizations.Database.Configurations;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<OrganizationIdConverter>();
        
        builder.HasMany(x => x.Locations)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);
        
        builder.HasMany(x => x.Positions)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);
    }
}