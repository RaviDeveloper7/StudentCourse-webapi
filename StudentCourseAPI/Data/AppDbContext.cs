using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Product> products { get; set; }

        public DbSet<Department> departments { get; set; }
    }
}
