using Day02.Models;

namespace Day02.ViewModels
{
    public class DepartmentViewModel
    {
        public string DeptName { get; set; }
        public List<Student> Students { get; set; }
        public string Status { get; set; }
    }
}
