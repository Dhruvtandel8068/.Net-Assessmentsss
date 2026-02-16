using Microsoft.EntityFrameworkCore;
using Assessment7.Models; // Your models namespace

namespace Assessment7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } // Your table
    }
}
