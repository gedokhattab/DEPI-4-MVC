using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Day02.Models
{
    public class Student
    {

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{3,}$", ErrorMessage = "Name must be letters only and at least 3 characters")]
        public string Name { get; set; } = null!;

        [Range(18, 50, ErrorMessage = "Age must be between 18 and 50")]
        public int? Age { get; set; }

        public int? DepartmentId { get; set; }

        [ValidateNever]
        public Department? Department { get; set; }

        [ValidateNever]
        public List<StudentCourseResult> StuCrsRes { get; set; }
    }
}
