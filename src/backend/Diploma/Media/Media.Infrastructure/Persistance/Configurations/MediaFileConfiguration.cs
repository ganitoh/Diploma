using Media.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Media.Infrastructure.Persistance.Configurations;

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FileName).IsRequired();
        builder.Property(x => x.ObjectKey).IsRequired();
        builder.Property(x => x.ContentType).IsRequired();
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.CreateAtUtc).IsRequired();
        builder.Property(x => x.DeletedAtUtc).IsRequired(false);
    }
}