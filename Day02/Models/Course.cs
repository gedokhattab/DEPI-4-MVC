using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day02.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is mandatory")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(50, 100)]
        public int Degree { get; set; }

        [Range(0, 100)]
        public int MinDegree { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [ValidateNever]
        public Department? Department { get; set; }
        [ValidateNever]
        public List<Teacher> Teachers { get; set; } = new();
        [ValidateNever]
        public List<StudentCourseResult> StuCrsRes { get; set; } = new();
    }
}
