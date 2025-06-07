using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>, IEntityTypeConfiguration<RatingCommentary>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable(nameof(Rating).ToLower(), "organization");
        builder.HasKey(x => x.Id);
        
        builder
            .HasMany(x => x.Commentaries)
            .WithOne(x => x.Rating)
            .HasForeignKey(x => x.RatingId);
    }

    public void Configure(EntityTypeBuilder<RatingCommentary> builder)
    {
        builder.ToTable(nameof(RatingCommentary).ToLower(), "organization");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Commentary).HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.RatingValue).IsRequired();
        
        builder
            .HasOne(x => x.Rating)
            .WithMany(x => x.Commentaries)
            .HasForeignKey(x => x.RatingId);
    }
}