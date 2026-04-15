using Day07_Assessment.Domain.Models;

namespace Day07_Assessment.Domain.Repository_Interfaces
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(TaskItem task);
        List<TaskItem> GetAll();
        TaskItem GetById(int id);
        void Save();
        IQueryable<TaskItem> SearchByName(string title);
        IQueryable<TaskItem> FilterCompleted(IQueryable<TaskItem> query, bool isCompleted);
        List<TaskItem> Paginate(IQueryable<TaskItem> query, int n);
    }
}
