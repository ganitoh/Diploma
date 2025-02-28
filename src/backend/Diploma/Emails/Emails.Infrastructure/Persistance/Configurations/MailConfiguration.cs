using Emails.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emails.Infrastructure.Persistance.Configurations;

public class MailConfiguration : IEntityTypeConfiguration<Mail>
{
    public void Configure(EntityTypeBuilder<Mail> builder)
    {
        builder.ToTable(nameof(Mail));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.Subject).IsRequired().HasMaxLength(250);  
        builder.Property(x => x.To).IsRequired().HasMaxLength(250);
        builder.Property(x => x.From).IsRequired().HasMaxLength(250);
    }
}