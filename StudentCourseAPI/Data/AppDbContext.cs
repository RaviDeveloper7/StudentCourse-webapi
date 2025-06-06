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

        public DbSet<Employee> employees { get; set; }

        public DbSet<EmployeeDetail> employeeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeDetail)
                .WithOne(d => d.Employee)
                .HasForeignKey<EmployeeDetail>(d => d.EmployeeId);
        }
    }
}