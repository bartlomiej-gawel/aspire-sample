using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Users.Features.ActivationTokens;

namespace Sample.Modules.Users.Database.Configurations;

internal sealed class ActivationTokenEntityConfiguration : IEntityTypeConfiguration<ActivationToken>
{
    public void Configure(EntityTypeBuilder<ActivationToken> builder)
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

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ExpireAt)
            .IsRequired();
    }
}