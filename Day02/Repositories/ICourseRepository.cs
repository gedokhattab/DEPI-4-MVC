using Day02.Models;

namespace Day02.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Update(Course course);
        void Delete(Course course);
        void Save();
        List<Course> GetAll();
        Course GetById(int id);
        IQueryable<Course> GetAllWithLoading();
        int GetCount(IQueryable<Course> query);
        IQueryable<Course> FilterDepartments(IQueryable<Course> query, int? deptId);
        IQueryable<Course> SearchByName(IQueryable<Course> query, string name);
        List<Course> Paginate(IQueryable<Course> query, int n);
        int AvgDegree(int id);
        void UpdateTeachers(Course course, List<int> selectedTeacherIds);
    }
}
