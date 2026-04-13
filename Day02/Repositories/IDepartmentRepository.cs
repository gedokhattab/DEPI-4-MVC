using Day02.Models;

namespace Day02.Repositories
{
    public interface IDepartmentRepository
    {
        public void Add(Department department);
        public void Update(Department department);
        public void Delete(Department department);
        public void Save();
        public List<Department> GetAll();
        public Department GetById(int id);
        public List<Department> GetAllWithLoading();
    }
}
