using Day02.Data.Contexts;
using Day02.Models;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        SchoolDbContext _context;
        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public void Add(Course course)
        {
            _context.Add(course);
        }

        public void Update(Course course)
        {
            _context.Update(course);
        }
        public void Delete(Course course)
        {
            _context.Remove(course);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }
        public Course GetById(int id)
        {
            return _context.Courses
                           .Include(C => C.Department)
                           .Include(C => C.Teachers)
                           .Include(C => C.StuCrsRes)
                           .ThenInclude(SCR => SCR.Student)
                           .FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<Course> GetAllWithLoading()
        {
            return _context.Courses
                           .Include(C => C.Department)
                           .Include(C => C.Teachers)
                           .AsNoTracking();
        }

        public int GetCount(IQueryable<Course> query)
        {
            return query.Count();
        }

        public int AvgDegree(int id)
        {
            return (int)(_context.StudentCourseResults
                                 .Where(SCR => SCR.CourseId == id)
                                 .Average(SCR => (int?)SCR.Grade) ?? 0);
        }

        public IQueryable<Course> SearchByName(IQueryable<Course> query, string name)
        {
            if (!string.IsNullOrEmpty(name))
                query = query.Where(C => C.Name.Contains(name));
            return query;
        }

        public IQueryable<Course> FilterDepartments(IQueryable<Course> query, int? deptId)
        {
            if (deptId.HasValue)
                query = query.Where(C => C.DepartmentId == deptId.Value);

            return query;
        }
        public void UpdateTeachers(Course course, List<int> selectedTeacherIds)
        {
            var selectedTeachers = _context.Teachers
                                           .Where(t => selectedTeacherIds.Contains(t.Id))
                                           .ToList();
            course.Teachers = selectedTeachers;
        }

        public List<Course> Paginate(IQueryable<Course> query, int n)
        {
            return query.Skip(n * 10)
                        .Take(10)
                        .ToList();
        }
    }
}
