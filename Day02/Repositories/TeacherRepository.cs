using Day02.Data.Contexts;
using Day02.Models;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        SchoolDbContext _context;
        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public void Add(Teacher teacher)
        {
            _context.Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            _context.Update(teacher);
        }
        public void Delete(Teacher teacher)
        {
            _context.Remove(teacher);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }
        public Teacher GetById(int id)
        {
            return _context.Teachers
                           .Include(T => T.Department)
                           .Include(T => T.Course)
                           .FirstOrDefault(T => T.Id == id);
        }
        public List<Teacher> GetCourseTeachers(int id)
        {
            return _context.Teachers
                           .Where(T => T.DepartmentId== id)
                           .ToList();
        }
        public IQueryable<Teacher> GetAllWithLoading()
        {
            return _context.Teachers
                           .Include(T => T.Department)
                           .Include(T => T.Course)
                           .AsNoTracking();
        }

        public int GetCount(IQueryable<Teacher> query)
        {
            return query.Count();
        }

        public IQueryable<Teacher> SearchByName(IQueryable<Teacher> query, string name)
        {
            if (!string.IsNullOrEmpty(name))
                query = query.Where(T => T.Name.Contains(name));
            return query;
        }

        public IQueryable<Teacher> FilterDepartments(IQueryable<Teacher> query, int? deptId)
        {
            if (deptId.HasValue)
                query = query.Where(T => T.DepartmentId == deptId.Value);

            return query;
        }
        
        public List<Teacher> Paginate(IQueryable<Teacher> query, int n)
        {
            return query.Skip(n * 10)
                        .Take(10)
                        .ToList();
        }
    }
}
