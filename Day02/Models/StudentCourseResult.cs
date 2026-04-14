using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day02.Models
{
    public class StudentCourseResult
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ValidateNever]
        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ValidateNever]
        public Course Course { get; set; }

        [Range(0, 100)]
        public int Grade { get; set; }
    }
}
