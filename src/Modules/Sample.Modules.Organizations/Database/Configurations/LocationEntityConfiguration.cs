using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Organizations.Features.Locations;

namespace Sample.Modules.Organizations.Database.Configurations;

internal sealed class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.OrganizationId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.ComplexProperty(x => x.Address, propertyBuilder =>
        {
            propertyBuilder.Property(x => x.Street)
                .HasMaxLength(255)
                .IsRequired();

            propertyBuilder.Property(x => x.City)
                .HasMaxLength(255)
                .IsRequired();

            propertyBuilder.Property(x => x.PostalCode)
                .HasMaxLength(10)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Location)
            .HasForeignKey(x => x.LocationId);
    }
}