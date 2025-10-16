using Microsoft.EntityFrameworkCore;
using Assignment_day06.Models;

namespace Assignment_day06.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
