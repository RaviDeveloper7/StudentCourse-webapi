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

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One: Employee - EmployeeDetail
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeDetail)
                .WithOne(d => d.Employee)
                .HasForeignKey<EmployeeDetail>(d => d.EmployeeId);

            // One-to-Many: Employee - TaskItem
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId);
        }
    }
}