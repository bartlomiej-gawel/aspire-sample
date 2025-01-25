using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Modules.Communications.Features.Messages;

namespace Sample.Modules.Communications.Database.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RecipientId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.Recipient)
            .WithMany()
            .HasForeignKey(x => x.RecipientId);

        builder.Property(x => x.Subject)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Channel)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}