using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Day07_Assessment.Domain.Models
{
    public class TaskItem
    {
        public int Id{ get; set; }

        [Required]
        [MaxLength(100)]
        public string Title{ get; set; }

        [MaxLength(500)]
        public string? Description{ get; set; }

        [DefaultValue(false)]
        [DisplayName("Completed")]
        public bool IsCompleted{ get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt{ get; set; }

        [DisplayName("Due Date")]
        public DateTime? DueTo{ get; set; }

    }
}
