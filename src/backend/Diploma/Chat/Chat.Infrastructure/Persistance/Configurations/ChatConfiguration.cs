using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistance.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Domain.Models.Chat>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Chat> builder)
    {
        builder.ToTable(nameof(Domain.Models.Chat).ToLower(), "chat");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstUserId).IsRequired();
        builder.Property(x => x.SecondUserId).IsRequired();

        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);
    }
}