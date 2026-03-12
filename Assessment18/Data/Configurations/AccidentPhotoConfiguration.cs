using Assessment18.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment18.Data.Configurations;

public class AccidentPhotoConfiguration : IEntityTypeConfiguration<AccidentPhoto>
{
    public void Configure(EntityTypeBuilder<AccidentPhoto> builder)
    {
        builder.ToTable("AccidentPhotos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileUrl).IsRequired().HasMaxLength(400);

        builder.HasOne(x => x.AccidentReport)
            .WithMany(a => a.Photos)
            .HasForeignKey(x => x.AccidentReportId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AccidentReportId);
    }
}