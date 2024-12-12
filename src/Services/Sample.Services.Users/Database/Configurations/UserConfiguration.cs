using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Users.Features.Users;

namespace Sample.Services.Users.Database.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.OrganizationId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.OrganizationName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Surname)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Phone)
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(x => x.Password)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}