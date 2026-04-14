using System.ComponentModel.DataAnnotations;

namespace Day02.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Manager Name")]
        [RegularExpression(@"^[a-zA-Z\s]{3,}$", ErrorMessage = "Name must be letters only and at least 3 characters")]
        public string? ManagerName { get; set; }

        public List<Teacher> Teachers { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}
