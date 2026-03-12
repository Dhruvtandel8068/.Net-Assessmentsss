using Assessment18.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment18.Data.Configurations;

public class AccidentConfiguration : IEntityTypeConfiguration<AccidentReport>
{
    public void Configure(EntityTypeBuilder<AccidentReport> builder)
    {
        builder.ToTable("AccidentReports");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OccurredAtUtc).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(u => u.AccidentReports)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.GeoLocation)
            .WithMany()
            .HasForeignKey(x => x.GeoLocationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.OccurredAtUtc);
    }
}