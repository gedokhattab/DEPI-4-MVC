using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Day02.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        public int? DepartmentId { get; set; }
        [ValidateNever]
        public Department? Department { get; set; }
        [ValidateNever]
        public List<StudentCourseResult> StuCrsRes { get; set; }
    }
}
