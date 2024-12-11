using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Services.Users.Features.RefreshTokens;

namespace Sample.Services.Users.Database.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.UserId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
        
        builder.Property(x => x.Value)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(x => x.Value)
            .IsUnique();

        builder.Property(x => x.ExpireAt)
            .IsRequired();
    }
}