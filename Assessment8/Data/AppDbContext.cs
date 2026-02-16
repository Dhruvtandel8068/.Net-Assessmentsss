using Microsoft.EntityFrameworkCore;
using Assessment8.Models;  // Make sure this line exists

namespace Assessment8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // <-- THIS IS THE IMPORTANT PART
        public DbSet<Student> Students { get; set; } = null!;  // Add this line
    }
}
