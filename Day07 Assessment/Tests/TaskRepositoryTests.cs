using Day07_Assessment.Data.Repositories_Implementations;
using Day07_Assessment.Domain.Models;
using Day07_Assessment.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day07_Assessment.Tests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        private TaskDbContext _context;
        private TaskRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            // Configure In-Memory Database with a unique name per test
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TaskDbContext(options);
            _repository = new TaskRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void Add_Should_Increase_Count()
        {
            // Arrange
            var task = new TaskItem { Id = 1, Title = "Test Task" };

            // Act
            _repository.Add(task);
            _repository.Save();

            // Assert
            Assert.AreEqual(1, _context.Tasks.Count());
        }

        [TestMethod]
        public void GetById_Should_Return_Correct_Task()
        {
            // Arrange
            var task = new TaskItem { Id = 5, Title = "Find Me" };
            _context.Tasks.Add(task);
            _context.SaveChanges();

            // Act
            var result = _repository.GetById(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Find Me", result.Title);
        }

        [TestMethod]
        public void SearchByName_Should_Filter_Correctly()
        {
            // Arrange
            _context.Tasks.AddRange(new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Apple" },
                new TaskItem { Id = 2, Title = "Banana" },
                new TaskItem { Id = 3, Title = "Appliance" }
            });
            _context.SaveChanges();

            // Act
            var result = _repository.SearchByName("App").ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(t => t.Title == "Apple"));
        }

        [TestMethod]
        public void FilterCompleted_Should_Only_Return_Completed_When_True()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "T1", IsCompleted = true },
                new TaskItem { Id = 2, Title = "T2", IsCompleted = false }
            }.AsQueryable();

            // Act
            var result = _repository.FilterCompleted(tasks, true).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.First().IsCompleted);
        }

        [TestMethod]
        public void Paginate_Should_Return_Correct_Batch_Size()
        {
            // Arrange
            for (int i = 1; i <= 25; i++)
            {
                _context.Tasks.Add(new TaskItem { Id = i, Title = $"Task {i}" });
            }
            _context.SaveChanges();
            var query = _context.Tasks.AsQueryable();

            // Act - Page 1 (index 1) should skip 10 and take 10
            var result = _repository.Paginate(query, 1);

            // Assert
            Assert.AreEqual(10, result.Count);
        }

        [TestMethod]
        public void Delete_Should_Remove_Task()
        {
            // Arrange
            var task = new TaskItem { Id = 10, Title = "Delete Me" };
            _context.Tasks.Add(task);
            _context.SaveChanges();

            // Act
            _repository.Delete(task);
            _repository.Save();

            // Assert
            Assert.AreEqual(0, _context.Tasks.Count());
        }
    }
}