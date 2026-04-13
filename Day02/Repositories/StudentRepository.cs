using Day02.Data.Contexts;
using Day02.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day02.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        SchoolDbContext _context;
        public StudentRepository()
        {
            _context = new SchoolDbContext();
        }
        public void Add(Student student)
        {
            _context.Add(student);
        }

        public void Update(Student student)
        {
            _context.Update(student);
        }
        public void Delete(Student student)
        {
            _context.Remove(student);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
        public Student GetById(int id)
        {
            return _context.Students
                           .Include(S => S.Department)
                           .Include(S => S.StuCrsRes)
                           .ThenInclude(SCR => SCR.Course)
                           .FirstOrDefault(S => S.Id == id);
        }
        public IQueryable<Student> GetAllWithLoading()
        {
            return _context.Students
                           .Include(S => S.Department)
                           .AsNoTracking();
        }


        public int GetCount(IQueryable<Student> query)
        {
            return query.Count();
        }

        public IQueryable<Student> SearchByName(IQueryable<Student> query, string name)
        {
            if(!string.IsNullOrEmpty(name))
                query = query.Where(s => s.Name.Contains(name));
            return query;
        }

        public IQueryable<Student> FilterDepartments(IQueryable<Student> query, int? deptId)
        {
            if (deptId.HasValue)
                query = query.Where(s => s.DepartmentId == deptId.Value);

            return query;
        }
        public List<Student> Paginate(IQueryable<Student> query, int n)
        {
            return query.Skip(n * 10)
                        .Take(10)
                        .ToList();
        }
    }
}
