using Day02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Day02.Data.Configurations
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasOne(t => t.Course)
                   .WithMany(c => c.Teachers)
                   .HasForeignKey(t => t.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
