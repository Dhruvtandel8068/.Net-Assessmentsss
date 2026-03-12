using Assessment18.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment18.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email).IsUnique(); // data integrity
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(120);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(180);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
    }
}