using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Models;

namespace Notifications.Infrastructure.Persistance.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(nameof(Notification).ToLower(),"notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).HasMaxLength(512);
        builder.Property(x => x.UserId).IsRequired();
    }
}