namespace Day02.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<Teacher> Teachers { get; set; } = new();
        public List<StudentCourseResult> StuCrsRes { get; set; } = new();
    }
}
