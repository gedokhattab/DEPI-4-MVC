using Day02.Models;

namespace Day02.Repositories
{
    public interface ITeacherRepository
    {
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
        void Save();
        List<Teacher> GetAll();
        Teacher GetById(int id);
        int GetCount(IQueryable<Teacher> query);
        IQueryable<Teacher> GetAllWithLoading();
        IQueryable<Teacher> SearchByName(IQueryable<Teacher> query, string name);
        IQueryable<Teacher> FilterDepartments(IQueryable<Teacher> query, int? deptId);
        List<Teacher> Paginate(IQueryable<Teacher> query, int n);
        List<Teacher> GetCourseTeachers(int id);
    }
}
