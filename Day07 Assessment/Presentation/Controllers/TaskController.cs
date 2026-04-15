using Day07_Assessment.Domain.Models;
using Day07_Assessment.Domain.Repository_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Day07_Assessment.Presentation.Controllers
{
    public class TaskController : Controller
    {
        ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: /Task
        [HttpGet]
        public IActionResult Index(string title = "", int n = 0, bool showCompleted = false)
        {
            ViewData["SearchTerm"] = title;
            var query = _taskRepository.SearchByName(title);
            query = _taskRepository.FilterCompleted(query, showCompleted);

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / 10.0);

            if (n >= totalPages && totalPages > 0)
                n = 0;

            var tasks = _taskRepository.Paginate(query, n);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = n;
            ViewData["IsCompleted"] = showCompleted;

            return View(tasks);
        }

        // GET: /Task/ShowDetails/1
        [HttpGet]
        public IActionResult ShowDetails(int id)
        {
            TaskItem task = _taskRepository.GetById(id);
            return View("ShowDetails", task);
        }

        // GET: /Task/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Add(task);
                _taskRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", task);
        }

        // Get: /Task/Edit/1
        [HttpGet]
        public IActionResult Edit(int id)
        {
            TaskItem task = _taskRepository.GetById(id);
            return View("Edit", task);
        }

        [HttpPost]
        public IActionResult EditTask(TaskItem NewTask)
        {
            TaskItem task = _taskRepository.GetById(NewTask.Id);
            if (ModelState.IsValid)
            {
                task.Title = NewTask.Title;
                task.Description = NewTask.Description;
                task.DueTo = NewTask.DueTo;
                task.IsCompleted = NewTask.IsCompleted;
                _taskRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", task);
        }

        // Get: /Task/Delete/1
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TaskItem task = _taskRepository.GetById(id);
            return View("Delete", task);
        }

        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task != null)
            {
                _taskRepository.Delete(task);
                _taskRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
