using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Models;

namespace Notifications.Infrastructure.Persistance.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(nameof(Notification).ToLower(), "notification");
        builder.HasKey(x => x.Id);
        
        builder
            .Property(notification => notification.Email)
            .HasMaxLength(80);

        builder
            .Property(notification => notification.Title)
            .HasMaxLength(80);

        builder
            .Property(notification => notification.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}