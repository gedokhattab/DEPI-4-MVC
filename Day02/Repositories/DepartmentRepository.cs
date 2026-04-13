using Day02.Data.Contexts;
using Day02.Models;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        SchoolDbContext _context;
        public DepartmentRepository()
        {
            _context = new SchoolDbContext();
        }
        public void Add(Department department)
        {
            _context.Add(department);
        }

        public void Update(Department department)
        {
            _context.Update(department);
        }
        public void Delete(Department department)
        {
            _context.Remove(department);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public List<Department> GetAllWithLoading()
        {
            return _context.Departments
                           .Include(D => D.Students)
                           .Include(D => D.Courses)
                           .Include(D => D.Teachers)
                           .ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments
                           .Include(D => D.Students)
                           .FirstOrDefault(S => S.Id == id);
        }
    }
}
