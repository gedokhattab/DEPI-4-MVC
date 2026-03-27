using Day02.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Day02.Models
{
    public class DepartmentBL
    {
        SchoolDbContext db = new SchoolDbContext();

        public List<Department> GetAll()
        {
            return db.Departments
                     .Include(D => D.Students)
                     .ToList();
        }

        public Department? GetById(int id)
        {
            return db.Departments
                     .Include(D => D.Students)
                     .FirstOrDefault(D => D.Id == id);
        }

        public void Add(Department dept)
        {
            db.Departments.Add(dept);
            db.SaveChanges();
        }
    }
}
