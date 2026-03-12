using Assessment18.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment18.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<NotificationLog>
{
    public void Configure(EntityTypeBuilder<NotificationLog> builder)
    {
        builder.ToTable("NotificationLogs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Recipient).IsRequired().HasMaxLength(120);
        builder.Property(x => x.Message).HasMaxLength(500);

        builder.HasOne(x => x.AccidentReport)
            .WithMany(a => a.Notifications)
            .HasForeignKey(x => x.AccidentReportId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AccidentReportId);
        builder.HasIndex(x => x.CreatedAtUtc);
    }
}