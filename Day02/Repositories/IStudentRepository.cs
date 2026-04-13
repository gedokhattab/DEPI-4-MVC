using Day02.Models;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repositories
{
    public interface IStudentRepository
    {
        public void Add(Student student);
        public void Update(Student student);
        public void Delete(Student student);
        public void Save();
        public List<Student> GetAll();
        public Student GetById(int id);
        public IQueryable<Student> GetAllWithLoading();
        public int GetCount(IQueryable<Student> query);
        public IQueryable<Student> SearchByName(IQueryable<Student> query, string name);
        public IQueryable<Student> FilterDepartments(IQueryable<Student> query, int? deptId);
        public List<Student> Paginate(IQueryable<Student> query, int n);
    }
}
