using Microsoft.EntityFrameworkCore;
using Assessment9.Models;

namespace Assessment9.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Rename table to "StudentTable" (or whatever you want)
            modelBuilder.Entity<Student>().ToTable("StudentTable");
        }
    }
}
