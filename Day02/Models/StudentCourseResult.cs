namespace Day02.Models
{
    public class StudentCourseResult
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int Grade { get; set; }
    }
}
