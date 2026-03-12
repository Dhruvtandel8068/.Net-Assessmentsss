using Assessment18.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment18.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AccidentReport> AccidentReports => Set<AccidentReport>();
    public DbSet<GeoLocation> GeoLocations => Set<GeoLocation>();
    public DbSet<EmergencyContact> EmergencyContacts => Set<EmergencyContact>();
    public DbSet<AccidentPhoto> AccidentPhotos => Set<AccidentPhoto>();
    public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Fluent Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}