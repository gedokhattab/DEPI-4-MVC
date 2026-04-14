using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day02.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{3,}$", ErrorMessage = "Name must be letters only and at least 3 characters")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Range(5000, 50000)]
        public decimal Salary { get; set; }

        [RegularExpression(@"(Alex|Cairo|Giza)", ErrorMessage = "Address must be Alex, Cairo, or Giza")]
        public string? Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [ValidateNever]
        public Department Department { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        [ValidateNever]
        public Course? Course { get; set; }
    }
}
