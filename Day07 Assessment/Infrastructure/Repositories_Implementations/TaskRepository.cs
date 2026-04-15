using Day07_Assessment.Domain.Models;
using Day07_Assessment.Domain.Repository_Interfaces;
using Day07_Assessment.Infrastructure.Data.DbContexts;

namespace Day07_Assessment.Data.Repositories_Implementations
{
    public class TaskRepository : ITaskRepository
    {
        TaskDbContext _context;
        public TaskRepository(TaskDbContext context) 
        { 
            _context = context;
        }

        public void Add(TaskItem task)
        {
            _context.Add(task);
        }

        public void Update(TaskItem task)
        {
            _context.Update(task);
        }
        public void Delete(TaskItem task)
        {
            _context.Remove(task);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<TaskItem> GetAll()
        {
            return _context.Tasks.OrderByDescending(t => t.DueTo).ToList();
        }
        public TaskItem GetById(int id)
        {
            return _context.Tasks
                           .FirstOrDefault(t => t.Id == id);
        }
       
        public IQueryable<TaskItem> SearchByName(string title)
        {
            if (!string.IsNullOrEmpty(title))
                return _context.Tasks.Where(t => t.Title.Contains(title));
            return _context.Tasks;
        }

        public IQueryable<TaskItem> SortByDueTo(IQueryable<TaskItem> query)
        {
            return query.OrderBy(t => t.DueTo);
        }

        public IQueryable<TaskItem> FilterCompleted(IQueryable<TaskItem> query, bool isCompleted)
        {
            if (isCompleted)
                return query.Where(t => t.IsCompleted == isCompleted);
            return query;
        }

        public List<TaskItem> Paginate(IQueryable<TaskItem> query, int n)
        {
            return query.Skip(n * 10)
                        .Take(10)
                        .ToList();
        }
    }
}
