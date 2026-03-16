using Day02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Day02.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
            

            builder.HasMany(d => d.Teachers)
                   .WithOne(t => t.Department)
                   .HasForeignKey(t => t.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Courses)
                   .WithOne(c => c.Department)
                   .HasForeignKey(c => c.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Students)
                   .WithOne(s => s.Department)
                   .HasForeignKey(s => s.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
