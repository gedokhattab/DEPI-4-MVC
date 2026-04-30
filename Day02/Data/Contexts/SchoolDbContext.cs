using Microsoft.EntityFrameworkCore;
using Day02.Models;

namespace Day02.Data.Contexts
{
    public class SchoolDbContext : DbContext
    {
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourseResult> StudentCourseResults { get; set; }

        public SchoolDbContext(DbContextOptions options) : base(options)
        {
            // Connection String Security Best Practices:
            // To fix the security hazard in connection strings,
            // swap hardcoded strings for Environment Variables so credentials stay on the host server and out of your source code.
            // For production, leverage Secret Managers like Azure Key Vault to manage sensitive data with centralized encryption and managed identity access.
            // For local development, rely on User Secrets to store configuration in a machine-specific folder that never leaks into your Git repository.
        }
    }
}
