using Day07_Assessment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Day07_Assessment.Infrastructure.Data.DbContexts
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base() { }
        public TaskDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasKey(c => c.Id);

            modelBuilder.Entity<TaskItem>().Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            modelBuilder.Entity<TaskItem>().Property(c => c.Description)
                   .IsRequired(false)
                   .HasMaxLength(500);

            modelBuilder.Entity<TaskItem>().Property(c => c.CreatedAt)
                        .HasDefaultValueSql("GETDATE()");
        }

    }
}
