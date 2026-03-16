namespace Day02.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<StudentCourseResult> StuCrsRes { get; set; }
    }
}
