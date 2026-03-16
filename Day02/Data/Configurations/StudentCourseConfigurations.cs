using Day02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Day02.Data.Configurations
{
    public class StudentCourseConfigurations : IEntityTypeConfiguration<StudentCourseResult>
    {
        public void Configure(EntityTypeBuilder<StudentCourseResult> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId }); 

            builder.HasOne(sc => sc.Student)
                   .WithMany(s => s.StuCrsRes)
                   .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Course)
                   .WithMany(c => c.StuCrsRes)
                   .HasForeignKey(sc => sc.CourseId);
        }
    }
}
